var io = require("socket.io").listen(8000);

io.configure(function(){  
    io.set('log level', 2);
});

var socketRoom = {};


var userNames = {};
var userPos = {};
var userCharacter = {};
var userTeam={};



var buildingHP = {};

var isRun = false;

var timer1;
var timer2;



 var jarray ={
     
 };
 

/*{  jarray["room1"]["minionnames"]
   "room1": {"minionnames":"{"":""}", "socket":"bb"}, 
   "room2":  {"roomid":"room1", "lastName":"Smith"}, 
   "room3": {"roomid":"room1","lastName":"Jones"},
   "room4":  {"roomid":"room2","lastName":"Jones"}
    };*/
 
 
 console.log (jarray);
 
 //console.log("get emp:  "+jarray["room1"]["socketif"]);
 

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
                if(temp[1]== roomKey){
                    
                    socket.room = roomKey;
                    jarray[socket.room] = roomKey;
                    socket.join(roomKey);
                    socket.emit('createRoomRES',temp[0]);
                    socketRoom[socket.id] = roomKey;
                    console.log("i'm not first socket.id = "+jarray);
                    return;
                }
             }
         }
         
         // console.log("rooms: "+JSON.stringify(socketRoom));
         
       //  console.log(" "+JSON.stringify(jarray));
         
       //  console.log(" roomkey: "+JSON.stringify(roomKey));
         
          //console.log("iorooms: "+JSON.stringify(rooms));
         
         /*
            for(var i in minionNames){
                delete(minionNames[i]);
            }
            
            for(var i in minionPos){
                delete(minionPos[i])              
            }
           */ 
       
           socket.room = temp[1];
           socket.join(temp[1]);
           socketRoom[socket.room ] = temp[1];
         
         jarray[socket.room ] = socketRoom;
         
         socket.emit('createRoomRES',temp[0]);
       
        console.log(" "+JSON.stringify(jarray));
         
  
         socket.emit("youMaster",temp[0]);
         createMinion();
        
         function createMinion(){
             var minionNames={};
             var minionPos = {};
             
            timer1 = setInterval(redSender,1000);
            timer2= setInterval(blueSender,1000);
            var maxMinion =10;
            var currMinion=0;
            var redIdx=0;
            var blueIdx=0;
            
            function redSender(){
                redIdx++;
                var data = "29.0,50.0,30.0";
                minionNames["rm"+redIdx] = "rm"+redIdx;
                minionPos["rm"+redIdx] = data;   
                
               jarray[socket.room].minionNames= minionNames; 
               
               
               jarray[socket.room].minionPos = minionPos;
                
                data = "rm"+redIdx+":"+data;         
                //console.log("data = "+data);
                 io.sockets.in(socketRoom[socket.id]).emit("createRedMinionRES",data);
                 currMinion++;
                 if(currMinion>=maxMinion)
                    clearInterval(timer1);
                
                 console.log(" "+JSON.stringify(jarray));
            }
            
            function blueSender(){
                blueIdx++;
                var data = "75.0,50.0,70.0";
                minionNames["bm"+blueIdx] = "bm"+blueIdx;
                minionPos["bm"+blueIdx] = data;
                
                jarray[socket.room]["minionNames"]= minionNames;
               
               jarray[socket.room]["minionPos"]= minionPos;
                
                 console.log(" "+JSON.stringify(jarray));
                
                data = "bm"+blueIdx+":"+data;                
                 io.sockets.in(socket.room).emit("createBlueMinionRES",data);
                 currMinion++;
                 if(currMinion>=maxMinion)
                    clearInterval(timer2);
            }
        }//end create minion
        
          console.log(" "+JSON.stringify(jarray));
        
    });//end create room
    
    

    socket.on("createPlayerREQ", function(data) {
        var ret = data.split(":");
        
        io.sockets.in(socketRoom[socket.id]).emit("createPlayerRES", data);
        
        userNames[socket.id] = ret[0];        
	userPos[ret[0]] = ret[1];
        userCharacter[ret[0]] = ret[2];
        userTeam[ret[0]] = ret[3];
//add to player in room        
         jarray[socket.room]["userNames"] = userNames;
          jarray[socket.room]["userPos"] = userPos;
           jarray[socket.room]["userCharacter"] = userCharacter;
            jarray[socket.room]["userTeam"] = userTeam;
         
          console.log("createplayer: ret"+ret[0]);
 
         console.log(""+JSON.stringify(jarray));
    });
    
    socket.on("preuserREQ", function(data){
        var ret1=data+'=';
        var ret2=data+'=';
       
        for(var key in jarray[socket.room]["minionNames"]){
            ret1 += minionNames[key]+":"+minionPos[minionNames[key]]+"_";
        }
        
        for(var key in jarray[socket.room]["minionNames"]){
            ret2 += jarray[socket.room]["userNames"][key]+":"+userPos[userNames[key]]+":"+userCharacter[userNames[key]]+":"+userTeam[userNames[key]]+"_";
        }
        io.sockets.in(socket.room).emit("preuser1RES",ret1);
        io.sockets.in(socket.room).emit("preuser2RES",ret2);
    });
    
    socket.on("movePlayerREQ",function(data){   
        var ret = data.split(":");
        
        
        userPos[ret[0]] = ret[1];
        io.sockets.in(socket.room).emit("movePlayerRES", data);  
        
       // console.log(ret[0] +": "+ret[1]);
    });
    
    socket.on("attackREQ",function(data){
        io.sockets.in(socket.room).emit("attackRES", data); 
    });
    
    socket.on("moveSyncREQ",function(data){  
        var ret = data.split(":");
        userPos[ret[0]] = ret[1];         
        io.sockets.in(socket.room).emit("moveSyncRES", data);
        
      //  console.log("ret: "+ ret[0]);
        
    });
    
    socket.on("minionAttackREQ",function(data){
        io.sockets.in(socket.room).emit("minionAttackRES", data); 
    });
    
    socket.on("minionSyncREQ",function(data){  
        if(data !=null){//edit?   
            io.sockets.in(socket.room).emit("minionSyncRES", data);
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