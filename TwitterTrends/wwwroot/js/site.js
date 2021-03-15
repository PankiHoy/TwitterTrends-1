var DrawState = new function() {
    var that = this,
        nodes,
        context,
        points = [];
   

    var drawCanvasPoints = function(State){
        context.fillStyle = State.getColor();
        context.lineWidth = 1;
        context.strokeStyle = 'rgb(0,0,0)';
        context.beginPath();
        let mas = []; mas = State.getPolygons().toArray;
        mas.array.forEach(function(mas, i) {
            if (i == 0) {
                context.moveTo(mas[0], mas[1]);
            }
            else {
                context.lineTo(mas[0], mas[1]);
            }
        });
        context.closePath();
        context.fill();
        context.stroke();
    }

    that.init = function() {
        nodes = {
            'draw' : _.getElement('draw'),
            'canvas' : _.getElement('canvas')
        };
        nodes['canvas'].width = nodes['draw'].offsetWidth;
        nodes['canvas'].height = nodes['draw'].offsetHeight;
        context = nodes['canvas'].getContext('2d');
        clearBtn();
        renderBtn();
    }




}