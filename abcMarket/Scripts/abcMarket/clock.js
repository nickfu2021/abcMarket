function starttime() {
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    h = checktime(h);
    m = checktime(m);
    s = checktime(s);
    document.getElementById("clock").innerHTML = h + ":" + m + ":" + s;
    var time = setTimeout(starttime, 500);

    function checktime(i) {
      if (i < 10) {
        i = "0" + i
      }
      return i;
    }
  }