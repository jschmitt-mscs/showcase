const Customer = require('../classes/Customer');
const { getInvoiceLineItemsAsync } = require('../classes/functions');
const functions = require('../classes/functions');

module.exports = function(app){

    //get customer
    app.get('/api/customer/:customerID', function(req,res){
        var customer1 = new Customer(req.params.customerID);
        customer1.getFromDB().then(
            data => {
                res.send(JSON.parse(JSON.stringify(data[0])));
            }
        )
    })

    app.post('/api/newCustomer',function(req,res){
        var cust_fname;
        var cust_lname;
        var cust_street;
        var cust_city;
        var cust_state;
        var cust_zip;
        var cust_balance;

        if(!req.query){
            cust_fname = req.body.cust_fname;
            cust_lname = req.body.cust_lname;
            cust_street = req.body.cust_street;
            cust_city = req.body.cust_city;
            cust_state = req.body.cust_state;
            cust_zip = req.body.cust_zip;
            cust_balance = req.body.cust_balance;

        } else {
            cust_fname = req.query.cust_fname;
            cust_lname = req.query.cust_lname;
            cust_street = req.query.cust_street;
            cust_city = req.query.cust_city;
            cust_state = req.query.cust_state;
            cust_zip = req.query.cust_zip;
            cust_balance = req.query.cust_balance;
        }
        var customer1 = new Customer(cust_fname, cust_lname, cust_street, cust_city,
            cust_state, cust_zip, cust_balance);
        
        customer1.addToDBAsync().then(
            data => {
                res.send(data);
            }
        )
    })

    app.get('/api/invoices/:inv_num', function(req,res){
        getInvoiceLineItemsAsync(req.params.inv_num).then(
            data => {
                res.send(JSON.parse(JSON.stringify(data)));
            }
        )
    });

    app.get('/api/products', function(req,res){
        var prodFilter = {

        }
        var prod_base = req.query.prod_base;
        var prod_type = req.query.prod_type;
        var prod_category = req.query.prod_category;
        if(prod_base != 'All'){
            prodFilter.prod_base = prod_base;
        }
        if(prod_type != 'All'){
            prodFilter.prod_type = prod_type;
        }
        if(prod_category != 'All'){
            prodFilter.prod_category = prod_category;
        }
        console.log(JSON.stringify(prodFilter));
        functions.getProductFilteredAsync(prodFilter).then(
            prod_data => {
                res.send(JSON.parse(JSON.stringify(prod_data)));
            }
        )


    })

}
