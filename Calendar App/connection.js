var mysql = require('mysql');
var con = mysql.createConnection({
    host: "192.168.1.15",
    user: "joepc1",
    password: "X$z6^90,X",
    database: "CalendarApp",
    multipleStatements: true
});

con.connect(function(err) {
    if (err) {
        console.log(err);
    } else {
        console.log("Connected!");
    }
});

module.exports = con;