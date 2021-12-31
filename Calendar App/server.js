var express = require("express");
var app = express();
var ejs = require("ejs");
app.set('view engine', 'ejs');
app.use(express.static(__dirname + '/public'));
var bodyParser = require('body-parser');
app.use(express.json());
app.use(bodyParser.urlencoded({ extended: true }));

require('./routes')(app);
app.listen(45000);