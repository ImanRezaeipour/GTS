function color_changed(sender, args) {
	if (sender.get_selectedColor() && sender.get_selectedColor().get_hex()) {
		var c = "#" + sender.get_selectedColor().get_hex();
		document.getElementById("chip").style.backgroundColor = c;
		document.getElementById("hex").innerHTML = c;
		document.getElementById("clr_ColorPicker").style.backgroundColor = c;
	}
}

function DialogColors_OnClose(sender, e) {
	document.getElementById("chip").style.backgroundColor = '';
	document.getElementById("hex").innerHTML = '';
}

function DialogColors_OnShow(sender, e) {
	document.getElementById('DialogColors').style.zIndex = 25000000;
}

function ShowDialogColors() {
	DialogColors.Show();
}