//------------------跨域无认证或者Logout用这个
function Send(url, objBody) {
    $.ajax({
        url: url,
        type: "post",
        data: JSON.stringify(objBody),
        contentType: "application/json; charset=UTF-8",
        success: function (result) {
            alert("data = " + JSON.stringify(result));
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
};
var url="http://localhost:5000/api/Values";
var objBody={id:1,name:'web',booknum:12};
//----------------------------------------------
var url="http://localhost:5000/api/Index"   //加验证会跳转
//----------------------------如果login 是post方法
var url="http://localhost:5000/api/Account";
var objBody={userName:'halyhuang',password:'web'};
//----------跨域有认证
function Send(url, objBody) {
    $.ajax({
        url: url,
        type: "post",
        data: objBody,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        xhrFields: {
            withCredentials: true
         },
        success: function (result) {
            alert("data = " + JSON.stringify(result));
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
};
//---------------------------------withCredentials: true
document.cookie='userName=halyhuang';
document.cookie='password=web';
var url="http://localhost:5000/api/Index";
