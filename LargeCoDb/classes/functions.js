const { json } = require('express');
var sqlString = require('sqlString');
var con = require('../connection');
module.exports = {

    getInvoiceLineItemsAsync: function (inv_num) {
        return new Promise((resolve, reject) => {
            var sql = 'SELECT * FROM LGLINE WHERE' +
                ' INV_NUM = ' +
                sqlString.escape((inv_num)) +
                ' ORDER BY LINE_NUM;'
            con.query(sql, (err, response) => {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(response);
                }
            });
        });
    },

    getProductAsync: function (prod_sku) {
        return new Promise((resolve, reject) => {
            var sql = 'SELECT * FROM LGPRODUCT ' +
                'WHERE PROD_SKU = ' +
                sqlString.escape((prod_sku)) + ';';
            con.query(sql, (err, response) => {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(response);
                }
            });
        });
    },

    getAllProductAsync: function () {
        return new Promise((resolve, reject) => {
            var sql = 'SELECT * FROM LGPRODUCT';
            con.query(sql, (err, response) => {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(response);
                }
            });
        });
    },

    getProductByProdTypeAsync: function (prod_type) {
        return new Promise((resolve, reject) => {
            var sql = 'SELECT * FROM LGPRODUCT ' +
                'WHERE PROD_TYPE = ' +
                sqlString.escape(prod_type) + ';';
            con.query(sql, (err, response) => {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(response);
                }
            });
        });
    },

    getProductByProdBaseAsync: function (prod_base) {
        return new Promise((resolve, reject) => {
            var sql = 'SELECT * FROM LGPRODUCT ' +
                'WHERE PROD_BASE = ' +
                sqlString.escape(prod_base) + ';';
            con.query(sql, (err, response) => {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(response);
                }
            });
        });
    },

    getProductTypesAsync: function () {
        return new Promise((resolve, reject) => {
            var sql = 'SELECT DISTINCT(PROD_TYPE) ' +
                'FROM LGPRODUCT;'
            con.query(sql, (err, response) => {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(response);
                }
            });
        });
    },

    getProductCategoriesAsync: function () {
        return new Promise((resolve, reject) => {
            var sql = 'SELECT DISTINCT(PROD_CATEGORY) ' +
                'FROM LGPRODUCT;'
            con.query(sql, (err, response) => {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(response);
                }
            });
        });
    },

    getProductBasesAsync: function () {
        return new Promise((resolve, reject) => {
            var sql = 'SELECT DISTINCT(PROD_BASE) ' +
                'FROM LGPRODUCT;'
            con.query(sql, (err, response) => {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(response);
                }
            });
        });
    },

    getProductFilteredAsync: function (prodFilter) {

        return new Promise((resolve, reject) => {
            var sql = 'SELECT * FROM LGPRODUCT';
            var keys = Object.keys(prodFilter);
            if (!prodFilter || keys.length == 0) {
                sql = 'SELECT * FROM LGPRODUCT;';
            } else if (keys.length == 1) {
                var key = keys[0];
                if (prodFilter.hasOwnProperty(key)) {
                    sql += ' WHERE ' +
                        key +
                        ' = ' +
                        sqlString.escape(prodFilter[key]) + ';'
                }
                console.log(sql);
            } else {
                var firstKey = keys[0];
                sql += ' WHERE ' +
                    firstKey +
                    ' = ' +
                    sqlString.escape(prodFilter[firstKey]);
                for (var i = 1; i < keys.length; i++) {
                    var nextKey = keys[i];
                    sql += ' AND ' + nextKey + ' = ' +
                        sqlString.escape(prodFilter[nextKey]);
                }
                sql += ';';
            }
            con.query(sql, (err, response) => {
                if (err) {
                    return reject(err);
                } else {
                    return resolve(response);
                }
            });
        });

    },

    testFunction(jsonObject) {
        console.log(Object.keys(jsonObject).length);
        for (var key in jsonObject) {
            if (jsonObject.hasOwnProperty(key)) {
                console.log(key + " " + jsonObject[key]);
            }
        }
    }


}