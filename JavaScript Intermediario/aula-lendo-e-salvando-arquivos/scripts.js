function handleFileSelect(){
	var myFile = document.querySelector('#myFile').files[0];

	var reader = new FileReader();

	reader.onload = function(){
		var content = reader.result;
		document.querySelector('#fileContent').value = content;
	}

	reader.readAsText(myFile);
}


function saveFile(){
	var element = document.createElement('a'),
		content = document.querySelector('#fileContent').value;

	element.setAttribute('href', 'data:plain/text;charse=utf-8,'+ encodeURIComponent(content) );
	element.setAttribute('download', 'myNewFile.txt');
	element.click();
}