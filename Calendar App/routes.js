
const sqlString = require('sqlString');
var con = require('./connection');


module.exports = function(app){
    app.get('/', function(req, res) {

        let sql = 'SELECT * FROM Event';
        con.query(sql, function(err, result){
            if(err){
                res.send(err);
            } else {
                result = JSON.parse(JSON.stringify(result));
                res.render('pages/index', {
                    pagename: 'home',
                    data: result
                });
            }
        });
    });

    app.post("/newEvent", function(req,res){

        let startDate = new Date(req.body.eventStartDay + " " + req.body.eventStartTime);
        startDate.setMinutes(startDate.getMinutes() - 360);
        let endDate = new Date(req.body.eventEndDay + " " + req.body.eventEndTime);
        endDate.setMinutes(endDate.getMinutes() -360);

        var sql = 'INSERT INTO Event (Name, Description, Start, End) VALUES (' +
        sqlString.escape(req.body.eventTitle) + ', ' +
        sqlString.escape(req.body.eventDescription) + ', ' +
        sqlString.escape(startDate) + ', ' +
        sqlString.escape(endDate) + ')';

        con.query(sql, function(err, result){
            if(err) console.log(err);
            console.log("One record inserted")
        })


        console.log(req.body);
        res.writeHead(302,{
            'Location': '/',
        });
        res.end();
    })
    
    app.get('/api/events', function(req, res) {

        let sql = 'SELECT * FROM Event';
        con.query(sql, function(err, result){
            if(err){
                res.send(err);
            } else {
                result = JSON.parse(JSON.stringify(result));
                res.send(result);
            }
        });
    });
}