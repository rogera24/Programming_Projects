function minimize(varx){
	var parent = varx.parentNode;
	var x = parent.children;
	if(x[1].style.display != "none"){
		x[1].style.display = "none";
	}
    else{
		x[1].style.display = "block";
	}
	console.log("done");
}

function ZedSet(varx){
	var old = varx.style.zIndex;
	var window = document.getElementById("chatroom_body").children;
	for (i = 0; i < window.length; i++) { 
		if(window[i].style.zIndex > old){
			window[i].style.zIndex = window[i].style.zIndex - 10;
			console.log(window[i].style.zIndex);
		}
	}
	varx.style.zIndex = 30;
	console.log("Z-Index has been reduced on all greater windows.");
}

function drag(ev, window){
	ZedSet(window);
	var win_id = window.getAttribute('id');
	console.log(win_id);
	var sexy = getComputedStyle(window);
	var X = parseInt(sexy.getPropertyValue("left")) - event.clientX;
	console.log(X);
	var Y = parseInt(sexy.getPropertyValue("top")) - event.clientY;
	console.log(Y);
	event.dataTransfer.setData("Text", win_id + ',' + X + ',' + Y);
}

function drop(ev){
	var offset = event.dataTransfer.getData("text/plain").split(',');
	var dm = document.getElementById(offset[0]);
	dm.style.left = (event.clientX + parseInt(offset[1],10)) + 'px';
	dm.style.top = (event.clientY + parseInt(offset[2],10)) + 'px';
	event.preventDefault();
	return false;
}

function drag_over(ev){
	ev.preventDefault();
	return false;
}

document.body.addEventListener('dragover',drag_over,false);
document.body.addEventListener('drop',drop,false); 