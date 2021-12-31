var express = require("express");
var app = express();
var ejs = require("ejs");
app.set('view engine', 'ejs');
const path = require('path');
const Brand = require('./classes/Brand');
const Customer = require('./classes/Customer');

var bodyParser = require('body-parser');
const { testFunction, getProductFilteredAsync } = require("./classes/functions");
const { json } = require("express");
app.use(express.json());
app.use(bodyParser.urlencoded({ extended: true }));

//public documents
app.use(express.static(__dirname + '/public'));

//bootstrap loading
app.use('/css', express.static(path.join(__dirname, 'node_modules/bootstrap/dist/css')))
app.use('/js', express.static(path.join(__dirname, 'node_modules/bootstrap/dist/js')))


require('./routes/api')(app);
require('./routes/routes')(app);
app.listen(45000);

var jsonObject = {
    prod_category: 'Top Coat'
}  
console.log(!jsonObject.prod_cost);