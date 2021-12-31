var con = require('../connection');
var sqlString = require('sqlString');

module.exports = class Brand {


    constructor(brand_id, brand_name, brand_type) {
        this.brand_id = brand_id;
        this.brand_name = brand_name;
        this.brand_type = brand_type;
        this.updateDB();
    }

    static BrandTypes = {
        VALUE: "VALUE",
        PREMIUM: "PREMIUM",
        CONTRACTOR: "CONTRACTOR"
    }

    getBrandID(){
        return this.brand_id;
    }

    getBrandName(){
        return this.brand_name;
    }

    getBrandType(){
        return this.brand_type;
    }

    addToDB(){
        var sql = 'INSERT INTO LGBRAND (brand_id, brand_name, brand_type)' +
        'VALUES (' +
        sqlString.escape(this.brand_id) + ", " +
        sqlString.escape(this.brand_name) + ", " +
        sqlString.escape(this.brand_type) + ")";

        con.query(sql,function(err, result){
            if(err) console.log(err);
            console.log("New Brand Inserted:\t" + (this.brand_name))
        })
    }
}