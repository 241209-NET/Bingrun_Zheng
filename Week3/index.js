function getRandomColor() {
    const colors = ['black', 'red', 'blue', 'purple','gray','yellow'];
    return colors[Math.floor(Math.random() * colors.length)];
}

document.getElementById('button1').addEventListener('click', function() {
    alert("Something Happened");
});

document.getElementById('button2').addEventListener('click', function() {
    this.style.color = getRandomColor();
});

document.getElementById('button3').addEventListener('click', function() {
    this.style.backgroundColor = getRandomColor();
});