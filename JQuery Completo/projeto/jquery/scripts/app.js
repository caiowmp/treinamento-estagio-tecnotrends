var listFilter = '',
	//listElement = document.getElementById('pokeList'),
	//inputElement = document.getElementById('pokeFilter'),
	//pokeballElement = document.getElementById('pokeballBack');
	listElement = $('#pokeList'),
	inputElement = $('#pokeFilter'),
	pokeballElement = $('#pokeballBack');

//inputElement.addEventListener('keyup', function(){
inputElement.on('keyup',function(){
	listFilter = this.value;
	setList();
})

//window.addEventListener('scroll', function(){
$(window).on('scroll', function(){
	var rotation = 'translateY(-50%) rotateZ(' + (window.scrollY / 15) + 'deg)';
	//pokeballElement.style.transform = rotation;
	pokeballElement.css('transform', rotation);
})

function setList(){
	PokeService.listAll(function(pkmList){
		var list = filterList(pkmList);
		var template = ListService.createList(list);
		//listElement.innerHTML = template;
		listElement.html(template);
	});
}

function filterList(pkmList){
	return pkmList.filter(function(pkm){
		return pkm.name.indexOf(listFilter.toLowerCase()) !== -1;
	})
}


setList();

//fazendo o plugin ser acionado apertando em qualquer lugar do elemento na lista
//e não só na foto
$('#pokeList').on('click', 'li', function(){
	$(this).find('img').jump();
})