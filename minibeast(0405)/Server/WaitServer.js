var io = require("socket.io").listen(8080);

io.configure(function(){  
    io.set('log level', 2);
});

var socketRoom = {};
var userNames = {};//id
var selectCharacter={};
var userNum={};//position in waitRoom

var thrashNum=[];//shoud be at the each room
var thrashIdx=0;//shoud be at the each room

for(thrashIdx=0;thrashIdx<6;thrashIdx++){
    thrashNum[thrashIdx] = 5-thrashIdx;
}
thrashIdx--;

io.sockets.on('connection', function (socket) {	      
    console.log("A user connected !");    

socket.on("createRoomREQ",function(data){
         var rooms = io.sockets.manager.rooms;
         for(var key in rooms){
             if(key==''){
                 continue;
             }
             
             if(rooms[key].length<6){//next man
                var roomKey = key.replace('/','');
                socket.join(roomKey);
                
                data=roomKey;
                socket.emit("createRoomRES",data);
                socketRoom[socket.id] = roomKey;
                console.log("i'm not first socket.id = "+socket.id);
                return;
             }
         }
         
         socket.join(socket.id);//first man
         socketRoom[socket.id] = socket.id;
         data = socket.id;
         socket.emit('createRoomRES',data);
         console.log("i'm first socket.id = "+socket.id);
    });
    
     socket.on("createPlayerREQ", function(data) {// data == id        
        userNames[socket.id] =data;
        userNum[data] = thrashNum[thrashIdx]
        thrashIdx--;
        
        selectCharacter[data] = 'random';
        
        data = userNum[data]+':'+data;
        io.sockets.in(socketRoom[socket.id]).emit("createPlayerRES", data);
    });
    
    socket.on("preuserREQ", function(data){
        var ret=data+'=';
        
        for(var key in selectCharacter){
            ret +=userNum[key]+':'+key+':'+selectCharacter[key]+'-';
        }
        
        io.sockets.in(socketRoom[socket.id]).emit("preuserRES",ret);
    });
    
    socket.on("characterSelectREQ", function(data){
        var temp=data.split(':');
        //userNum[temp[0]] = temp[1];
        selectCharacter[temp[0]] = temp[2];    
        
        var ret = temp[1]+':'+temp[2];        
        io.sockets.in(socketRoom[socket.id]).emit("characterSelectRES",ret);
    });
    
    socket.on('disconnect',function(data){
        var rooms = io.sockets.manager.rooms;
        var key = socketRoom[socket.id];
        
        if(key!=null){//if client did enter the room
             key = '/'+key;
            if(rooms[key].length<=1){
                for(var i in socketRoom){                
                    delete(socketRoom[i])
                 }
                 for(var i in userNames){
                    delete(userNames[i])                     
                }
                 for(var i in selectCharacter){
                    delete(selectCharacter[i])                     
                }
                for(var i in userNum){
                    delete(userNum[i])                     
                } 
                
                for(thrashIdx=0;thrashIdx<6;thrashIdx++){
                    thrashNum[thrashIdx] = 5-thrashIdx;
                }
                thrashIdx--;
            }else{
                var ret = userNum[userNames[socket.id]];
                thrashIdx++;
                thrashNum[thrashIdx] = ret;
                io.sockets.in(socketRoom[socket.id]).emit("imoutRES", ret);                
                delete(userNum[userNames[socket.id]]);
                delete(selectCharacter[userNames[socket.id]]);
                delete(socketRoom[socket.id]);
                delete(userNames[socket.id]);
                
            }
            socket.leave(key);
        }
    });
});

