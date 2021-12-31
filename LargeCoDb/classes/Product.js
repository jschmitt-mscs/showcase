var con = require('../connection');
var sqlString = require('sqlString');


module.exports = class Product {
    constructor(prod_sku, prod_descript, prod_type, 
        prod_base, prod_category, prod_price, prod_qoh,
        prod_min, brand_id){

            if(arguments.length == 1){
                this.prod_sku = arguments[0];
            }
    }

    getFromDBAsync(){
        return new Promise((resolve, reject) => {
            var sql = 'SELECT * FROM LGPRODUCT WHERE ' +
            'PROD_SKU = ' +
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
}