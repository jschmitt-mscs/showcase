var con = require('../connection');
var bodyParser = require('body-parser');
var urlencodedParser = bodyParser.urlencoded({ extended: false });
const Customer = require('../classes/Customer');
var sqlString = require('sqlString');
var functions = require('../classes/functions');

module.exports = function (app) {

    app.get('/', function (req, res) {
        res.render('pages/index', {
            page_name: 'home'
        });
    });

    app.get('/customer/:cust_code', function (req, res) {
        var cust1 = new Customer(req.params.cust_code);
        cust1.getFromDB().then(
            cust_data => {
                cust1.getCustomerInvoicesAsync().then(inv_data => {
                    
                    res.render('pages/customer', { 
                        page_name: 'home',
                        cust_data: JSON.parse(JSON.stringify(cust_data[0])),
                        inv_data: inv_data
                    });
                });
            });

    });

    app.get('/product', function(req, res) {
        functions.getProductTypesAsync().then(
            prod_types => {
                functions.getProductBasesAsync().then( 
                    prod_bases => {
                        functions.getProductCategoriesAsync().then(
                            prod_categories => {
                                functions.getAllProductAsync().then(
                                    prod_data => {
                                        res.render('pages/productTable', {
                                            page_name: 'Products',
                                            prod_types: prod_types,
                                            prod_bases: prod_bases,
                                            prod_categories: prod_categories,
                                            prod_data: prod_data
                                        });
                                    }
                                )

                            }
                        )
                    }
                )
            }
        )
    });
}