var io = require("socket.io").listen(3000);

io.configure(function(){  
    io.set('log level', 2);
});

var socketRoom = {};

var userNames = {};
var userPos = {};
var userCharacter = {};

var minionNames={};
var minionPos = {};

var buildingHP = {};

var isRun = false;

io.sockets.on('connection', function (socket) {		//Ŭ���̾�Ʈ�� ����Ǵ�???? ���???? �Լ��� �����Ѵ�.
    //socket.emit('news', { hello: 'world' });		//Ŭ���̾�Ʈ���� news���???? �̸����� JSON��Ʈ���� �����Ѵ�.

    console.log("A user connected !");
    
    socket.on("createRoomREQ",function(data){
        var temp = data.split(':');
         var rooms = io.sockets.manager.rooms;
         for(var key in rooms){
             if(key==''){
                 continue;
             }else{
                var roomKey = key.replace('/','');
                if(temp[1]==roomKey){
                    socket.join(roomKey);
                    socket.emit('createRoomRES',temp[0]);
                    socketRoom[socket.id] = roomKey;
                     console.log("i'm not first socket.id = "+socket.id);
                    return;
                }
             }
         }
         
         socket.join(temp[1]);
        // socketRoom[socket.id] = socket.id;
         socketRoom[socket.id] = temp[1];
         socket.emit('createRoomRES',temp[0]);
         console.log("i'm first socket.id = "+socket.id);
         console.log("socketRoom[socket.id] = "+socketRoom[socket.id]);
         console.log("id = " +temp[0]);
         
         socket.emit("youMaster",temp[0]);
         createMinion();
        
         function createMinion(){
            var timer = setInterval(sender,1000);
            var maxMinion =10;
            var currMinion=0;
            var idx=0;
            var data;
            function sender(){
                idx++;
                data = "29.0,50.0,30.0";
                minionNames[idx] = "rm"+idx;
                minionPos["rm"+idx] = data;
                
                data = "rm"+idx+":"+data;
                
                 io.sockets.in(socketRoom[socket.id]).emit("createMinionRES",data);
                 currMinion++;
                 if(currMinion>=maxMinion)
                    clearInterval(timer);
            }
        }
    });

    socket.on("createPlayerREQ", function(data) {
        var ret = data.split(":");
        
        io.sockets.in(socketRoom[socket.id]).emit("createPlayerRES", data);
        
        userNames[socket.id] = ret[0];
	userPos[ret[0]] = ret[1];
        userCharacter[ret[0]] = ret[2];
        
        for(var key in userNames){
            console.log(userNames[key]);
        }
    });
    
    socket.on("preuserREQ", function(data){
        var ret1=data+'=';
        var ret2=data+'=';
       
        for(var key in minionNames){
            ret1 += minionNames[key]+":"+minionPos[minionNames[key]]+"_";
        }
        
        for(var key in userNames){
            ret2 += userNames[key]+":"+userPos[userNames[key]]+":"+userCharacter[userNames[key]]+"_";
        }
        console.log(ret2);
        io.sockets.in(socketRoom[socket.id]).emit("preuser1RES",ret1);
        io.sockets.in(socketRoom[socket.id]).emit("preuser2RES",ret2);
    });
    
    socket.on("movePlayerREQ",function(data){   
        var ret = data.split(":");
        userPos[ret[0]] = ret[1];
        io.sockets.in(socketRoom[socket.id]).emit("movePlayerRES", data);   
    });
    
    socket.on("attackREQ",function(data){
        io.sockets.in(socketRoom[socket.id]).emit("attackRES", data); 
    });
    
    socket.on("moveSyncREQ",function(data){  
        var ret = data.split(":");
        userPos[ret[0]] = ret[1];         
        io.sockets.in(socketRoom[socket.id]).emit("moveSyncRES", data);
    });
    
    socket.on("minionSyncREQ",function(data){  
        if(data !=null){//edit?   
            io.sockets.in(socketRoom[socket.id]).emit("minionSyncRES", data);
        }
    });
    
    socket.on('disconnect',function(data){
        var rooms = io.sockets.manager.rooms;
        var key = socketRoom[socket.id];
        
        if(key!=null){//if client did enter the room
        key = '/'+key;
        //console.log("key = "+key);
        if(rooms[key].length<=1){
            for(var i in minionNames){                
                delete(minionNames[i])
            }
            for(var i in minionPos){
                delete(minionPos[i])                
            }
        } 
            
        var ret = userNames[socket.id];
        io.sockets.in(socketRoom[socket.id]).emit("imoutRES", ret);
        delete(userPos[userNames[socket.id]]);
        delete(userNames[socket.id]);
        delete(socketRoom[socket.id]);
        socket.leave(key);
        }
    });


socket.on('attackMinion', function(data){
 var ret2 = data.split(":");
        buildingHP[ret2[0]] = ret2[1];         
        io.sockets.in(socketRoom[socket.id]).emit("attackMinion", data);

});

    socket.on('attackBuilding', function(data){
        var ret2 = data.split(":");
        buildingHP[ret2[0]] = ret2[1];         
        io.sockets.in(socketRoom[socket.id]).emit("attackBuilding", data);
    });

});