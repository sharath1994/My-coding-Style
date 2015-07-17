var app = require('http').createServer(handler);
var io = require('socket.io').listen(app);
var SerialPort = require("serialport").SerialPort;
var fs = require('fs');
var commonStatus = 'ON';

app.listen(80);

var sp = new SerialPort("COM8", {
  baudrate: 9600
});
 
 sp.on('open',function() {
      console.log('SerialPort is Open');
  });
	
	sp.on('data', function(data) {
      console.log('data received: ' + data);
});

function handler (req, res) {
  fs.readFile(__dirname + '/final.html',
  function (err, data) {
    if (err) {
      res.writeHead(500);
      return res.end('Error loading Page');
    }

    res.writeHead(200);
    res.end(data);
  });
}

io.sockets.on('connection', function (socket) {
    
    //Send client with his socket id
    socket.emit('your id', 
        { id: socket.id});
    
    //Info all clients a new client connected
    /* io.sockets.emit('on connection', 
        { client: socket.id,
          clientCount: io.sockets.clients().length,
        }); */
		
		 socket.on('led', function (data) {
        console.log(data.status);
        
        //acknowledge with inverted status, 
        //to toggle button text in client
        if(data.status == 'on'){
		 commonStatus = 'ON';
		 sp.write('1');
		}
		else
		{
		commonStatus = 'OFF';
		 sp.write('0');
		}
		 io.sockets.emit('ledupdate', 
            { status: commonStatus,
              by: socket.id
            });
	     });
			
			    //Info all clients if this client disconnect
/*     socket.on('disconnect', function () {
        io.sockets.emit('on disconnect', 
            { 
			client: socket.id,
            clientCount: io.sockets.clients().length-1,
         });
    }); */
});