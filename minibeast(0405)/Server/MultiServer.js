var io = require("socket.io").listen(5000);

io.configure(function(){  
    io.set('log level', 2);
});

var socketRoom = {};

var userNames = {};
var userPos = {};
var userCharacter = {};
var userTeam={};

var minionNames={};
var minionPos = {};

var buildingHP = {};

var isRun = false;

var timer1;
var timer2;



 var jarray = [
    {"firstName":"John", "lastName":"Doe"}, 
    {"firstName":"Anna", "lastName":"Smith"}, 
    {"firstName":"Peter","lastName":"Jones"}
];
 
 
 console.log (jarray);
 
 console.log("get emp:  "+jarray[0]["firstName"]);
 

io.sockets.on('connection', function (socket) {		
    //socket.emit('news', { hello: 'world' });		

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
                    socketRoom["roomKey"+key] = roomKey;
                     console.log("i'm not first socket.id = "+socket.id);
                    return;
                }
             }
         }
         
          console.log("rooms: "+JSON.stringify(socketRoom));
         
          //console.log("iorooms: "+JSON.stringify(rooms));
         
         /*
            for(var i in minionNames){
                delete(minionNames[i]);
            }
            
            for(var i in minionPos){
                delete(minionPos[i])              
            }
           */ 
         socket.join(temp[1]);
         socketRoom[socket.id] = temp[1];
         socket.emit('createRoomRES',temp[0]);
         /*console.log("i'm first socket.id = "+socket.id);
         console.log("socketRoom[socket.id] = "+socketRoom[socket.id]);
         console.log("id = " +temp[0]);
         */
         
         socket.emit("youMaster",temp[0]);
         createMinion();
        
         function createMinion(){
            timer1 = setInterval(redSender,5000);
            timer2= setInterval(blueSender,5000);
            var maxMinion =10000;
            var currMinion=0;
            var redIdx=0;
            var blueIdx=0;
            
            function redSender(){
                redIdx++;
                var data = "29.0,50.0,30.0";
                minionNames["rm"+redIdx] = "rm"+redIdx;
                minionPos["rm"+redIdx] = data;   
                
                socketRoom["rm"+redIdx] = data;
                
                data = "rm"+redIdx+":"+data;         
                //console.log("data = "+data);
                 io.sockets.in(socketRoom[socket.id]).emit("createRedMinionRES",data);
                 currMinion++;
                 if(currMinion>=maxMinion)
                    clearInterval(timer1);
                
                 console.log("rooms: "+JSON.stringify(socketRoom));
            }
            
            function blueSender(){
                blueIdx++;
                var data = "75.0,50.0,70.0";
                minionNames["bm"+blueIdx] = "bm"+blueIdx;
                minionPos["bm"+blueIdx] = data;
                
                socketRoom["bm"+blueIdx] = data;
                
                 console.log("rooms: "+JSON.stringify(socketRoom));
                
                data = "bm"+blueIdx+":"+data;                
                 io.sockets.in(socketRoom[socket.id]).emit("createBlueMinionRES",data);
                 currMinion++;
                 if(currMinion>=maxMinion)
                    clearInterval(timer2);
            }
        }//end create minion
        
          console.log("rooms: "+JSON.stringify(socketRoom));
        
    });//end create room
    
    

    socket.on("createPlayerREQ", function(data) {
        var ret = data.split(":");
        
        io.sockets.in(socketRoom[socket.id]).emit("createPlayerRES", data);
        
        userNames[socket.id] = ret[0];
	userPos[ret[0]] = ret[1];
        userCharacter[ret[0]] = ret[2];
        userTeam[ret[0]] = ret[3];
    });
    
    socket.on("preuserREQ", function(data){
        var ret1=data+'=';
        var ret2=data+'=';
       
        for(var key in minionNames){
            ret1 += minionNames[key]+":"+minionPos[minionNames[key]]+"_";
        }
        
        for(var key in userNames){
            ret2 += userNames[key]+":"+userPos[userNames[key]]+":"+userCharacter[userNames[key]]+":"+userTeam[userNames[key]]+"_";
        }
        io.sockets.in(socketRoom[socket.id]).emit("preuser1RES",ret1);
        io.sockets.in(socketRoom[socket.id]).emit("preuser2RES",ret2);
    });
    
    socket.on("movePlayerREQ",function(data){   
        var ret = data.split(":");
        userPos[ret[0]] = ret[1];
        io.sockets.in(socketRoom[socket.id]).emit("movePlayerRES", data);  
        
       // console.log(ret[0] +": "+ret[1]);
    });
    
    socket.on("attackREQ",function(data){
        io.sockets.in(socketRoom[socket.id]).emit("attackRES", data); 
    });
    
    socket.on("moveSyncREQ",function(data){  
        var ret = data.split(":");
        userPos[ret[0]] = ret[1];         
        io.sockets.in(socketRoom[socket.id]).emit("moveSyncRES", data);
        
        console.log("ret: "+ ret[0]);
        
    });
    
    socket.on("minionAttackREQ",function(data){
        io.sockets.in(socketRoom[socket.id]).emit("minionAttackRES", data); 
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
                delete(minionNames[i]);
            }
            
            for(var i in minionPos){
                delete(minionPos[i])              
            }
            for(var i in userNames){               
                 delete(userNames[i]);
            }
            
                for(var i in userPos){               
                 delete(userPos[i]);
            }
                        
            clearInterval(timer1);
            clearInterval(timer2);
        } 
       // console.log("minionNames length = "+minionNames.length);
            
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

    socket.on('SkillAttack', function(data){
            
       io.sockets.in(socketRoom[socket.id]).emit("SkillAttack", data);
    });


});