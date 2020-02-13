// Write your Javascript code.
//geral
// Inicialização do Ajax
function ajaxInit(){
	var xmlhttp;
	
	try{
		xmlhttp = new XMLHttpRequest();
	}catch(ee){
		try{
			xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
		}catch(e){
			try{
				xmlhttp = newActiveXObject("Microsoft.XMLHTTP");
			}catch(E){
				xmlhttp = false;
			}
		}
	}
	
	return xmlhttp;
}
//for show Store Availability
function showStoreItemAvailability(url, objMessageId){
    var ajax=ajaxInit();
    if(ajax){
        console.log(url);
        url+="&rnd="+Math.random()*7;
        ajax.open("GET", url);
        //ajax.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        ajax.onreadystatechange = function () {
                                    if (ajax.readyState == 4){
                                        if(ajax.status == 200){
                                            //$("#"+objMessageId).innerHTML=ajax.responseText;
                                            document.getElementById(objMessageId).innerHTML=ajax.responseText;
                                            console.log(objMessageId);
                                        }
                                    }
                                }
        ajax.send("catalogItemId="+2);
    }
}
