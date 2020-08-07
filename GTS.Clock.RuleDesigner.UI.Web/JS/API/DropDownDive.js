
        var slideDownInitHeight = new Array();
        var slidedown_direction = new Array();

        var slidedownActive = false;
        var contentHeight = false;
        var slidedownSpeed = 3; 	
        var slidedownTimer = 7;
        
        function slidedown_showHide(boxId) {
            if (!slidedown_direction[boxId]) 
                 slidedown_direction[boxId] = 1;
             if (!slideDownInitHeight[boxId]) 
                 slideDownInitHeight[boxId] = 0;
             if (slideDownInitHeight[boxId] == 0)
                 slidedown_direction[boxId] = slidedownSpeed; 
             else slidedown_direction[boxId] = slidedownSpeed * -1;

            slidedownContentBox = document.getElementById(boxId);
            var subDivs = slidedownContentBox.getElementsByTagName('DIV');
            for (var no = 0; no < subDivs.length; no++) {
                if (subDivs[no].className == 'dhtmlgoodies_content') 
                    slidedownContent = subDivs[no];
            }

            contentHeight = slidedownContent.offsetHeight;

            slidedownContentBox.style.visibility = 'visible';
            slidedownActive = true;
            slidedown_showHide_start(slidedownContentBox, slidedownContent);
        }
        function slidedown_showHide_start(slidedownContentBox, slidedownContent) {

            if (!slidedownActive) return;
                 slideDownInitHeight[slidedownContentBox.id] = slideDownInitHeight[slidedownContentBox.id] / 1 + slidedown_direction[slidedownContentBox.id];
            if (slideDownInitHeight[slidedownContentBox.id] <= 0) {
                slidedownActive = false;
                slidedownContentBox.style.visibility = 'hidden';
                slideDownInitHeight[slidedownContentBox.id] = 0;
            }
            if (slideDownInitHeight[slidedownContentBox.id] > contentHeight) {
                slidedownActive = false;
                slidedownContentBox.style.height = contentHeight + 'px';
            }
            else
                slidedownContentBox.style.height = slideDownInitHeight[slidedownContentBox.id] + 'px';
            //slidedownContent.style.top = slideDownInitHeight[slidedownContentBox.id] - contentHeight + 'px';

            setTimeout('slidedown_showHide_start(document.getElementById("' + slidedownContentBox.id + '"),document.getElementById("' + slidedownContent.id + '"))', slidedownTimer);
        }

        function setSlideDownSpeed(newSpeed) {
            slidedownSpeed = newSpeed;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        var slideDownInitHeight_surplus = new Array();
        var slidedown_direction_surplus = new Array();

        var slidedownActive_surplus = false;
        var contentHeight_surplus = false;
        var slidedownTimer_surplus = 7;
        function slidedown_showHide_surplus(boxId) {
            if (!slidedown_direction_surplus[boxId])
                slidedown_direction_surplus[boxId] = 1;
            if (!slideDownInitHeight_surplus[boxId])
                slideDownInitHeight_surplus[boxId] = 0;

            if (slideDownInitHeight_surplus[boxId] == 0)
                slidedown_direction_surplus[boxId] = slidedownSpeed;
            else slidedown_direction_surplus[boxId] = slidedownSpeed * -1;

            slidedownContentBox_surplus = document.getElementById(boxId);
            var subDivs_surplus = slidedownContentBox_surplus.getElementsByTagName('DIV');
            for (var no = 0; no < subDivs_surplus.length; no++) {
                if (subDivs_surplus[no].className == 'dhtmlgoodies_content')
                    slidedownContent_surplus = subDivs_surplus[no];
            }



            contentHeight_surplus = slidedownContent_surplus.offsetHeight;

            slidedownContentBox_surplus.style.visibility = 'visible';
            slidedownActive_surplus = true;
            slidedown_showHide_start_surplus(slidedownContentBox_surplus, slidedownContent_surplus);
        }
        function slidedown_showHide_start_surplus(slidedownContentBox_surplus, slidedownContent_surplus) {

            if (!slidedownActive_surplus) return;
            slideDownInitHeight_surplus[slidedownContentBox_surplus.id] = slideDownInitHeight_surplus[slidedownContentBox_surplus.id] / 1 + slidedown_direction_surplus[slidedownContentBox_surplus.id];
            if (slideDownInitHeight_surplus[slidedownContentBox_surplus.id] <= 0) {
                slidedownActive_surplus = false;
                slidedownContentBox_surplus.style.visibility = 'hidden';
                slideDownInitHeight_surplus[slidedownContentBox_surplus.id] = 0;
            }
            if (slideDownInitHeight_surplus[slidedownContentBox_surplus.id] > contentHeight_surplus) {
                slidedownActive_surplus = false;
            }
            slidedownContentBox_surplus.style.height = slideDownInitHeight_surplus[slidedownContentBox_surplus.id] + 'px';
            slidedownContent_surplus.style.top = slideDownInitHeight_surplus[slidedownContentBox_surplus.id] - contentHeight_surplus + 'px';

            setTimeout('slidedown_showHide_start_surplus(document.getElementById("' + slidedownContentBox_surplus.id + '"),document.getElementById("' + slidedownContent_surplus.id + '"))', slidedownTimer_surplus);
        }