

$(document).ready(
		function () {
		    $('#bottomDockMenu').Fisheye(
				{
				    maxWidth: 60,
				    items: 'a',
				    itemsText: 'span',
				    container: '.bottomDockMenu-container',
				    itemWidth: 40,
				    proximity: 80,
				    alignment: 'left',
				    valign: 'bottom',
				    halign: 'center'
				}
			)
		}
	);
