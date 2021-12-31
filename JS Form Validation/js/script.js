"use strict";

//declarations
const forms = document.querySelectorAll('form');
let formed = forms[0];

let username = document.getElementById("username");
let usernameErrorSpan = document.getElementById("usernameErrorSpan");

let password = document.getElementById("password");
let passwordVerify = document.getElementById("passwordVerify");
let passwordErrorSpan = document.getElementById("passwordErrorSpan");

let address = document.getElementById("address");
let address2 = document.getElementById("address2");
let city = document.getElementById("city");
let state = document.getElementById("state");
let zipcode = document.getElementById("zipcode");

let addressErrorSpan = document.getElementById("addressErrorSpan");
let address2ErrorSpan = document.getElementById("address2ErrorSpan");
let cityErrorSpan = document.getElementById("cityErrorSpan");
let stateErrorSpan = document.getElementById("stateErrorSpan");
let zipcodeErrorSpan = document.getElementById("zipcodeErrorSpan");

let billingAddress = document.getElementById("billingAddress");
let billingAddress2 = document.getElementById("billingAddress2");
let billingCity = document.getElementById("billingCity");
let billingState = document.getElementById("billingState");
let billingZipcode = document.getElementById("billingZipcode");

let billingAddressErrorSpan = document.getElementById("billingAddressErrorSpan");
let billingAddress2ErrorSpan = document.getElementById("billingAddress2ErrorSpan");
let billingCityErrorSpan = document.getElementById("billingCityErrorSpan");
let billingStateErrorSpan = document.getElementById("billingStateErrorSpan");
let billingZipcodeErrorSpan = document.getElementById("billingZipcodeErrorSpan");


let phone = document.getElementById("phone");
let email = document.getElementById("email");

let programmingSkillSelect = document.getElementById("programmingSkills");
let sameAddress = document.getElementById("sameAddress");


//special validation functions

function validateUsername() {

    let usernameValidity = username.validity.valid;

    if (!usernameValidity)
        usernameErrorSpan.innerText = "Please enter a valid email address";
    else
        usernameErrorSpan.innerText = "";
    username.valid = true;
}

function validatePasswords() {
    if (password.value != passwordVerify.value) {
        passwordErrorSpan.innerText = "Passwords must match";
        password.valid = false;
    } else {
        passwordErrorSpan.innerText = "";
        password.valid = true;
    }
}

function validateAddress() {
    if (!address.validity.valid) {
        addressErrorSpan.innerText = "Please enter an address."
        address.valid = false;
    } else {
        addressErrorSpan.innerText = ""
        address.valid = true;
    }
}

function validatePhone() {
    let regex = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/;
    if (regex.test(phone.value)) {
        phone.valid = true;
        phoneErrorSpan.innerText = "";
    } else {
        phone.valid = false;
        phoneErrorSpan.innerText = "Please enter a valid phone number.";
    }
}

function validateState() {
    var states = ["AK", "AL", "AR", "AS", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "GU", "HI", "IA",
        "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MH", "MI", "MN", "MO", "MS", "MT",
        "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "PR", "PW", "RI",
        "SC", "SD", "TN", "TX", "UT", "VA", "VI", "VT", "WA", "WI", "WV", "WY"];
    for (var i = 0; i < states.length; i++) {
        if (state.value.toUpperCase() == states[i]) {
            state.valid = true;
            stateErrorSpan.innerText = "";
            break;
        } else {
            state.valid = false;
            stateErrorSpan.innerText = "Please enter a valid state.";
        }
    }
}

function validateBillingState() {
    var states = ["AK", "AL", "AR", "AS", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "GU", "HI", "IA",
        "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MH", "MI", "MN", "MO", "MS", "MT",
        "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "PR", "PW", "RI",
        "SC", "SD", "TN", "TX", "UT", "VA", "VI", "VT", "WA", "WI", "WV", "WY"];
    for (var i = 0; i < states.length; i++) {
        if (billingState.value.toUpperCase() == states[i]) {
            billingState.valid = true;
            billingStateErrorSpan.innerText = "";
            break;
        } else {
            billingState.valid = false;
            billingStateErrorSpan.innerText = "Please enter a valid state.";
        }
    }
}

function validateZipcode() {
    let regex = /^\d{5}(?:[-\s]\d{4})?$/;
    if (regex.test(zipcode.value)) {
        zipcode.valid = true;
        zipcodeErrorSpan = "";
    } else {
        zipcodeErrorSpan = "Please enter a valid zip code. 55044 / 55044-9545 / 55044 1234"
        zipcode.valid = false;
    }
}

function validateBillingZipcode() {
    let regex = /^\d{5}(?:[-\s]\d{4})?$/;
    if (regex.test(billingZipcode.value)) {
        billingZipcode.valid = true;
        billingZipcodeErrorSpan = "";
    } else {
        billingZipcodeErrorSpan = "Please enter a valid zip code. 55044 / 55044-9545 / 55044 1234"
        billingZipcode.valid = false;
    }
}

function validateEmail() {

    let emailValidity = email.validity.valid;

    if (!emailValidity)
        emailErrorSpan.innerText = "Please enter a valid email address";
    else {
        emailErrorSpan.innerText = "";
        email.valid = true;
    }

}

//validation catch all
//this will catch required elements
function validateForm() {
    Array.from(formed.elements).forEach((element) => {
        if (!element.validity.valid && !!document.getElementById(element.id + "ErrorSpan")) {
            let errSpan = document.getElementById(element.id + "ErrorSpan");
            errSpan.innerText = "Please enter in a correct " + element.id;
        } else if (!!document.getElementById(element.id + "ErrorSpan")) {
            let errSpan = document.getElementById(element.id + "ErrorSpan");
            errSpan.innerText = "";
        }
    });
}

//EVENT LISTENERS
//any special validation will have two functions, the remainder will only have validateForm

username.onkeyup = function () {
    validateForm();
    validateUsername();
};
password.onkeyup = function () {
    validateForm();
    validatePasswords();
};
passwordVerify.onkeyup = function () {
    validateForm();
    validatePasswords();
};

address.onkeyup = validateForm;
address2.onkeyup = validateForm;
city.onkeyup = validateForm;
state.onkeyup = function () {
    validateForm();
    validateState();
}
zipcode.onkeyup = function () {
    validateForm();
    validateZipcode();
}

billingAddress.onkeyup = validateForm;
billingAddress2.onkeyup = validateForm;
billingCity.onkeyup = validateForm;
billingState.onkeyup = function () {
    validateForm();
    validateBillingState();
}
billingZipcode.onkeyup = function () {
    validateForm();
    validateBillingZipcode();
}

phone.onkeyup = function () {
    validateForm();
    validatePhone();
}

email.onkeyup = function () {
    validateForm();
    validateEmail();
}

sameAddress.addEventListener("change",function(){
    if(sameAddress.checked){
        billingAddress.value = address.value;
        billingAddress2.value = address2.value;
        billingCity.value = city.value;
        billingState.value = state.value;
        billingZipcode.value = zipcode.value;

        billingAddress.readOnly = true;
        billingAddress2.readOnly = true;
        billingCity.readOnly = true;
        billingState.readOnly = true;
        billingZipcode.readOnly = true;
    } else {
        billingAddress.value = "";
        billingAddress2.value = "";
        billingCity.value = "";
        billingState.value = "";
        billingZipcode.value = "";

        billingAddress.readOnly = false;
        billingAddress2.readOnly = false;
        billingCity.readOnly = false;
        billingState.readOnly = false;
        billingZipcode.readOnly = false;
    }
});

formed.addEventListener("submit",function(){
    if(sameAdresss.checked){
        billingAddress.value = address.value;
        billingAddress2.value = address2.value;
        billingCity.value = city.value;
        billingState.value = state.value;
        billingZipcode.value = zipcode.value;

    }
})