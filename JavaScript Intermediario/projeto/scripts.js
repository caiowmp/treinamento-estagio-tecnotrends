var field, player;

class Field{
    constructor (cols, rows, containerId){
        this.cols = cols;
        this.rows = rows;
        this.container = document.querySelector(containerId);
        this.createFiled();
    }

    createFiled () {
        var field = [];
        for(var i = 0; i < this.rows; i++){
            field[i] = [];
            for (var j = 0; j < this.cols; j++){
                field[i].push(this.createRock());
            }
        }
        this.field = field;
        this.drawField();
    }

    createRock () {
        return Math.trunc(Math.random() * 5) === 1 ? '@' : '';
    }

    drawField () {
        var template = '';
        for(var i = 0; i < this.rows; i++){
            template += '<tr>'; 
            for (var j = 0; j < this.cols; j++){
                template += `<td>${this.field[i][j]}</td>`;
            }
            template += '</tr>';
        }
        this.container.innerHTML = template;
    }
}

class Char {
    constructor (field, x, y, face) {
        this.face = face;
        this.x = x;
        this.y = y;
        this.table = field;
        if (!this.setPosition(x,y)) {
            throw Error();
        }
    }

    setPosition (x, y) {
        if (this.table.field[y][x] === ''){
            this.table.field[this.y][this.x] = '';
            this.x = x;
            this.y = y;
            this.table.field[this.y][this.x] = this.face;
            this.table.drawField();
            return true;
        }
        return false;
    }

    up () {
        if (this.y > 0){
            this.setPosition(this.x, this.y - 1);
        }
    }

    down () {
        if (this.y < this.table.rows - 1){
            this.setPosition(this.x, this.y + 1);
        }
    }

    left () {
        if (this.x > 0) {
            this.setPosition(this.x - 1, this.y);
        }
    }

    right () {
        if (this.x < this.table.cols - 1) {
            this.setPosition(this.x + 1, this.y);
        }
    }
}


class Player extends Char {
    constructor (field) {
        super (field, 0, 0, '0_0');
    }
}

function startField () {
    field = new Field(3,4,'#myTable');
    try {
        player = new Player (field);
    } catch (e) {
        console.log ('starting field again');
        startField();
    }
}

startField();