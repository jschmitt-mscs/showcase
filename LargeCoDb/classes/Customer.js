var con = require('../connection');
var sqlString = require('sqlString');


module.exports = class Customer {

    constructor(cust_fname, cust_lname, cust_street,
        cust_city, cust_state, cust_zip, cust_balance) {

            this.cust_code = 0;
            this.cust_fname = "";
            this.cust_lname = "";
            this.cust_street = "";
            this.cust_city = "";
            this.cust_state = "";
            this.cust_zip = "";
            this.cust_balance = 0; 

        if (arguments.length == 1) {
            this.cust_code = arguments[0];
        } else if (arguments.length == 7) {
            this.cust_fname = cust_fname;
            this.cust_lname = cust_lname;
            this.cust_street = cust_street;
            this.cust_city = cust_city;
            this.cust_state = cust_state;
            this.cust_zip = cust_zip;
            this.cust_balance = cust_balance;
        };
        //this.setAttributes();
    }

    setAttributes() {
        this.getFromDB().then((data) => {
            this.cust_fname = data[0].cust_fname;
            this.cust_lname = data[0].cust_lname;
            this.cust_street = data[0].cust_street;
            this.cust_city = data[0].cust_city;
            this.cust_state = data[0].cust_state;
            this.cust_zip = data[0].cust_zip;
            this.cust_balance = data[0].cust_balance;
            console.log(this);
        })
    }


    setCustCode(cust_code) {
        this.cust_code = cust_code;
    }

    setCustFName(cust_fname) {
        this.cust_fname = cust_fname;
    }

    setCustLName(cust_lname) {
        this.cust_lname = cust_lname;
    }

    setCustStreet(cust_street) {
        this.cust_street = cust_street;
    }

    setCustState(cust_state) {
        this.cust_state = cust_state;
    }

    setCustZip(cust_zip) {
        this.cust_zip = cust_zip;
    }

    setCustBalance(cust_balance) {
        this.cust_balance = cust_balance;
    }


    getCustCode() {
        return this.cust_code;
    }

    getCustFName() {
        return this.cust_fname;
    }

    getCustLName() {
        return this.cust_lname;
    }

    getCustStreet() {
        return this.cust_street;
    }

    getCustState() {
        return this.cust_state;
    }

    getCustZip() {
        return this.cust_zip;
    }

    getCustBalance() {
        return this.cust_balance;
    }


    updateDB() {
        var sql = 'UPDATE LGCUSTOMER ' +
            'SET cust_fname = ' + sqlString.escape(this.cust_fname) + ', ' +
            'cust_lname = ' + sqlString.escape(this.cust_lname) + ', ' +
            'cust_street = ' + sqlString.escape(this.cust_street) + ', ' +
            'cust_city = ' + sqlString.escape(this.cust_city) + ', ' +
            'cust_state = ' + sqlString.escape(this.cust_state) + ', ' +
            'cust_zip = ' + sqlString.escape(this.cust_zip) + ', ' +
            'cust_balance = ' + sqlString.escape(this.cust_balance) +
            'WHERE cust_code =' + sqlString.escape(this.cust_code) + ';';

        con.query(sql, function (err, result) {
            if (err) console.log(err);
            console.log(result);
        })
    }

    addToDB() {
        var sql = 'INSERT INTO LGCUSTOMER (' +
            'cust_fname, cust_lname, cust_street, cust_city, cust_state,' +
            'cust_zip, cust_balance) VALUES (' +
            sqlString.escape(this.cust_fname) + ', ' +
            sqlString.escape(this.cust_lname) + ', ' +
            sqlString.escape(this.cust_street) + ', ' +
            sqlString.escape(this.cust_city) + ', ' +
            sqlString.escape(this.cust_state) + ', ' +
            sqlString.escape(this.cust_zip) + ', ' +
            sqlString.escape(this.cust_balance) + ')';

        con.query(sql, function (err, result) {
            if (err) console.log(err);
            console.log("New Brand Inserted:\t" + (this.cust_fname));
        })
    }

    addToDBAsync(){
        return new Promise((resolve,reject) =>{
            var sql = 'INSERT INTO LGCUSTOMER (' +
            'cust_fname, cust_lname, cust_street, cust_city, cust_state,' +
            'cust_zip, cust_balance) VALUES (' +
            sqlString.escape(this.cust_fname) + ', ' +
            sqlString.escape(this.cust_lname) + ', ' +
            sqlString.escape(this.cust_street) + ', ' +
            sqlString.escape(this.cust_city) + ', ' +
            sqlString.escape(this.cust_state) + ', ' +
            sqlString.escape(this.cust_zip) + ', ' +
            sqlString.escape(this.cust_balance) + ')';

            con.query(sql, function (err, result) {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(result);
                }
            })

        })
    }

    getFromDB() {
        return new Promise((resolve, reject) => {
            var sql = 'SELECT * FROM LGCUSTOMER WHERE CUST_CODE = ' +
                sqlString.escape((this.cust_code)) + ';'
            con.query(sql, (err, response) => {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(response);
                }
            });
        });
    };

    removeFromDB() {
        var sql = 'DELETE FROM LGCUSTOMER WHERE cust_code = ' +
            sqlString.escape(this.cust_code) + ';'
        con.query(sql, function (err, result) {
            if (err) console.log(err);
            console.log(result);
        })
    }

    getCustomerInvoicesAsync() {
        return new Promise((resolve, reject) => {
            var sql = 'SELECT * FROM LGINVOICE WHERE CUST_CODE = ' +
                sqlString.escape((this.cust_code)) + ';'
            con.query(sql, (err, response) => {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(response);
                }
            });
        });
    }

    getCustomerInvoiceLineItemsAsync(inv_num){
        return new Promise((resolve, reject) => {
            var sql = 'SELECT * FROM LGLINE WHERE INV_NUM = ' +
                sqlString.escape((inv_num)) + ';'
            con.query(sql, (err, response) => {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(response);
                }
            });
        });
    }
}