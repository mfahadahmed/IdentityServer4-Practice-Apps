function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api-1").addEventListener("click", api1, false);
document.getElementById("api-2").addEventListener("click", api2, false);
document.getElementById("logout").addEventListener("click", logout, false);

var config = {
    authority: "https://localhost:6001",
    client_id: "js-client-3",
    redirect_uri: "http://localhost:5005/callback.html",
    response_type: "code",
    scope: "openid profile email api1 api2",
    post_logout_redirect_uri: "http://localhost:5005/index.html",
};

var mgr = new Oidc.UserManager(config);
mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}

function api1() {
    mgr.getUser().then(function (user) {
        var url = "https://localhost:8001/identity";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);

        xhr.onload = function () {
            if (xhr.status === 200) {
                log(xhr.status, JSON.parse(xhr.responseText));
            } else if (xhr.status === 401) {
                log(xhr.status, new Error("UnAuthorized Access to resource"));
            } else {
                log(xhr.status, new Error("Unknown error occurred while accessing resource"));
            }
        }

        xhr.setRequestHeader("Authorization", "Bearer " + user?.access_token);
        xhr.send();
    });
}

function api2() {
    mgr.getUser().then(function (user) {
        var url = "https://localhost:8002/identity";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);

        xhr.onload = function () {
            if (xhr.status === 200) {
                log(xhr.status, JSON.parse(xhr.responseText));
            } else if (xhr.status === 401) {
                log(xhr.status, new Error("UnAuthorized Access to resource"));
            } else {
                log(xhr.status, new Error("Unknown error occurred while accessing resource"));
            }
        }

        xhr.setRequestHeader("Authorization", "Bearer " + user?.access_token);
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}