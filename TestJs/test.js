var width = 500;
var height = 200;

var y = 0;



//	ベクタークラス
var Vector = function(x, y)
{
    this.x = x;
    this.y = y;
};

//	プライヤークラス
var Player = function(x, y)
{
    this.pos = new Vector(x, y);
    this.vel = new Vector(0.0, 0.0);
    
    //	更新処理
    this.update = function() {
        this.pos.x += this.vel.x;
        this.pos.y += this.vel.y;
    };
    
    //	描画処理
    this.drawing = function(ctx) {
    	ctx.fillStyle = "rgba(" + [255, 0, 0, 1] + ")";
		ctx.fillRect(this.pos.x, this.pos.y, 10, 10);
    };
};

var player = new Player(width/4, height/2);
player.vel.x = 0.4;

var cameraPos = new Vector(0.0, 0.0);


//	キャンバス描画処理
function draw()
{
	var ctx = document.getElementById('test').getContext('2d');
	
	ctx.beginPath();
	background(ctx);
	
	ctx.fillStyle = "rgba(" + [255, 255, 0, 1] + ")";
	ctx.fillRect(width/2, height, 10, y);
	y--;
	
	
	//	プレイヤー更新
	player.update();
	player.drawing(ctx);
	
	
	//	translateを使ってカメラのような挙動を入れたい
	ctx.translate(cameraPos.x - y*0.1, cameraPos.y);
// 	alert( "Error!!!" );
	
	window.requestAnimationFrame(draw);
}


//	背景更新
function background(ctx)
{
	ctx.fillStyle = "rgba(" + [100, 100, 100, 0.1] + ")";	//	ブラー風
	ctx.fillRect(0, 0, width, height);
}