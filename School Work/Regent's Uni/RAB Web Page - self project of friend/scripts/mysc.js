var content_num = 1;
function show_nav(){
	var nav = document.getElementById("pic_nav");
	nav.style.left = "0px";
}
function hide_nav(){
	var nav = document.getElementById("pic_nav");
	nav.style.left = "-170px";
}
function subject_change(num){
	content_num = num;
	console.log(content_num);
	if(content_num == 1){
		document.getElementById("picture_port").src = "images/venice/picture1.jpg";
		document.getElementById("picture_port").style.height = "517px";
		document.getElementById("picture_port").style.width = "385px";
		document.getElementById("view_port").style.marginTop = "85px";
		document.getElementById("view_port").style.marginLeft = "15%";
		document.getElementById("pic_title").innerHTML = "Venetian Church";
		document.getElementById("pic_desc").innerHTML = "The beauty behind Venice lies in the way it utilizes the space in which it if given. Venice, a small chain of islands has very limited space, but yet religion still finds its way to build extravagant large churches.";
	}
	else if(content_num == 2){
		document.getElementById("picture_port").src = "images/venice/picture2.jpg";
		document.getElementById("picture_port").style.height = "517px";
		document.getElementById("picture_port").style.width = "385px";
		document.getElementById("view_port").style.marginTop = "85px";
		document.getElementById("view_port").style.marginLeft = "15%";
		document.getElementById("pic_title").innerHTML = "The Alleys";
		document.getElementById("pic_desc").innerHTML = "In a large city, one would normally think that taking the back roads or alleys may not be the safest of choices but in Venice, most streets are back alleyways.";
	}
	else if(content_num == 3){
		document.getElementById("picture_port").src = "images/venice/picture3.jpg";
		document.getElementById("picture_port").style.height = "517px";
		document.getElementById("picture_port").style.width = "385px";
		document.getElementById("view_port").style.marginTop = "85px";
		document.getElementById("view_port").style.marginLeft = "15%";
		document.getElementById("pic_title").innerHTML = "The Road Less Travelled?";
		document.getElementById("pic_desc").innerHTML = "Turns out that side passageways are common along the water's side in Venice. Many times after crossing a bridge, one would be lead down through a side archway that leads out towards a main road.";
	}
	else if(content_num == 4){
		document.getElementById("picture_port").src = "images/venice/picture4.jpg";
		document.getElementById("picture_port").style.height = "517px";
		document.getElementById("picture_port").style.width = "385px";
		document.getElementById("view_port").style.marginTop = "85px";
		document.getElementById("view_port").style.marginLeft = "15%";
		document.getElementById("pic_title").innerHTML = "The Gathering Spots";
		document.getElementById("pic_desc").innerHTML = "In a city with very little space to put homes and businesses, there is still never a detract from the age old tradition of European markets in main squares like this one.";
	}
	else if(content_num == 5){
		document.getElementById("picture_port").src = "images/venice/picture5.jpg";
		document.getElementById("picture_port").style.width = "517px";
		document.getElementById("picture_port").style.height = "385px";
		document.getElementById("view_port").style.marginTop = "160px";
		document.getElementById("view_port").style.marginLeft = "10%";
		document.getElementById("pic_title").innerHTML = "Big Roads";
		document.getElementById("pic_desc").innerHTML = "In Venice there is about 4 main strips of waterways that but straight through the city, going in different directions. Even in these big roads, boats only number in the teens.";
	}
	else if(content_num == 6){
		document.getElementById("picture_port").src = "images/venice/picture6.jpg";
		document.getElementById("picture_port").style.width = "517px";
		document.getElementById("picture_port").style.height = "385px";
		document.getElementById("view_port").style.marginTop = "160px";
		document.getElementById("view_port").style.marginLeft = "10%";
		document.getElementById("pic_title").innerHTML = "The Main Drag";
		document.getElementById("pic_desc").innerHTML = "The beautiful thing about Venice is the way people go about their everyday lives. All across Europe, cars and trains are bustling across the surface, but the heart of Venice boats are the way to go.";
	}
	else if(content_num == 7){
		document.getElementById("picture_port").src = "images/venice/picture7.jpg";
		document.getElementById("picture_port").style.width = "517px";
		document.getElementById("picture_port").style.height = "385px";
		document.getElementById("view_port").style.marginTop = "160px";
		document.getElementById("view_port").style.marginLeft = "10%";
		document.getElementById("pic_title").innerHTML = "The Beauty of Bridges";
		document.getElementById("pic_desc").innerHTML = "Being a city of small island chains, there are bodies of water cutting all around the island. This gives way to the need to cross them by foot, and in turn beautiful bridges are all over the city.";
	}
	else if(content_num == 8){
		document.getElementById("picture_port").src = "images/venice/picture8.jpg";
		document.getElementById("picture_port").style.width = "517px";
		document.getElementById("picture_port").style.height = "385px";
		document.getElementById("view_port").style.marginTop = "160px";
		document.getElementById("view_port").style.marginLeft = "10%";
		document.getElementById("pic_title").innerHTML = "Boat Corrals";
		document.getElementById("pic_desc").innerHTML = "Throughout the city, there are giant logs jutting up through the waters surface, towering about the height of most people and sometimes eye level with others. Usually made of just a log, fancier ones can be found in the back roads.";
	}
	else if(content_num == 9){
		document.getElementById("picture_port").src = "images/venice/picture9.jpg";
		document.getElementById("picture_port").style.width = "517px";
		document.getElementById("picture_port").style.height = "385px";
		document.getElementById("view_port").style.marginTop = "160px";
		document.getElementById("view_port").style.marginLeft = "10%";
		document.getElementById("pic_title").innerHTML = "Private Docks";
		document.getElementById("pic_desc").innerHTML = "Many homes throughout Venice have small docks or back doors to the homes where normal people can just pull their boat up to their home and carry their goods inside.";
	}
	else if(content_num == 10){
		document.getElementById("picture_port").src = "images/venice/picture10.jpg";
		document.getElementById("picture_port").style.width = "517px";
		document.getElementById("picture_port").style.height = "385px";
		document.getElementById("view_port").style.marginTop = "160px";
		document.getElementById("view_port").style.marginLeft = "10%";
		document.getElementById("pic_title").innerHTML = "Dark History";
		document.getElementById("pic_desc").innerHTML = "Even in Venice, one of the most liberal cities, Venice still had a dislike for Jews and this is an ancient Roman district created for the purpose of Jewish living, away from the rest of the city.";
	}
	
}
function left(){
	content_num = content_num - 1;
	if(content_num <= 0){
		content_num = 1;
	}
	else{
		subject_change(content_num);
	}
}
function right(){
	content_num = content_num + 1;
	if(content_num > 10){
		content_num = 10;
	}
	else{
		subject_change(content_num);
	}
}
function shadow_on(side){
	var side;
	if(side == 1){
		side = document.getElementById("left_change");
		side.style.background = "grey";
		side.style.opacity = ".25";
	}
	else if(side == 2){
		side = document.getElementById("right_change");
		side.style.background = "grey";
		side.style.opacity = ".25";
	}
}
function shadow_off(side){
	var side;
	if(side == 1){
		side = document.getElementById("left_change");
		side.style.background = "none";
		side.style.opacity = "1.0";
	}
	else if(side == 2){
		side = document.getElementById("right_change");
		side.style.background = "none";
		side.style.opacity = "1.0";
	}
}