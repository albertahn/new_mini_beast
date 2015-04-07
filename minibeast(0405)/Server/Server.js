var io = require("socket.io").listen(3000);

io.configure(function(){  
    io.set('log level', 2);
});

var socketRoom = {};

var userNames = {};
var userPos = {};

var minionNames={};
var minionPos = {};


var isRun = false;

io.sockets.on('connection', function (socket) {		//Ŭ���̾�Ʈ�� ����Ǵ�??? ���??? �Լ��� �����Ѵ�.
    //socket.emit('news', { hello: 'world' });		//Ŭ���̾�Ʈ���� news���??? �̸����� JSON��Ʈ���� �����Ѵ�.

    console.log("A user connected !");
    
    socket.on("createRoomREQ",function(data){
         var rooms = io.sockets.manager.rooms;
         for(var key in rooms){
             if(key==''){
                 continue;
             }
             
             if(rooms[key].length<4){
                var roomKey = key.replace('/','');
                socket.join(roomKey);
                socket.emit('createRoomRES',data);
                socketRoom[socket.id] = roomKey;
                console.log("i'm not first socket.id = "+socket.id);
                return;
             }
         }
         
         socket.join(socket.id);
        // socketRoom[socket.id] = socket.id;
         socketRoom[socket.id] = socket.id;
         socket.emit('createRoomRES',data);
         console.log("i'm first socket.id = "+socket.id);
         console.log("socketRoom[socket.id] = "+socketRoom[socket.id]);
         
         socket.emit("youMaster",data);
         createMinion();
        
         function createMinion(){
             var timer = setInterval(sender,1000);
             var maxMinion =10;
             var currMinion=0;
             var idx=0;
             var data;
             function sender(){
                idx++;
                data = "48.9,52.47,33.58";
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
    });
    
    socket.on("preuserREQ", function(data){
        var ret1=data+'=';
        var ret2=data+'=';
       
        for(var key in minionNames){
            ret1 += minionNames[key]+":"+minionPos[minionNames[key]]+"_";
        }
        
        for(var key in userNames){
            ret2 += userNames[key]+":"+userPos[userNames[key]]+"_";
        }
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
        var ret = data.split(":");
        minionPos[ret[0]] = ret[1];         
        io.sockets.in(socketRoom[socket.id]).emit("minionSyncRES", data);
    });
    
    socket.on('disconnect',function(data){//Ŭ���̾�Ʈ�� ������ ���� ���??? �ڵ����� ȣ���???
        var key = socketRoom[socket.id];
        socket.leave(key);
        var ret = userNames[socket.id];
        io.sockets.in(socketRoom[socket.id]).emit("imoutRES", ret);
        delete(userPos[userNames[socket.id]]);
        delete(userNames[socket.id]);
        delete(socketRoom[socket.id]);
    });
});