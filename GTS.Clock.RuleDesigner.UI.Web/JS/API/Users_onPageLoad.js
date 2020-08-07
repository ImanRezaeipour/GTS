

$(document).ready
 (
  function () {
      // 1.
      parent.DialogLoading.Close();
      // 2.
      document.body.dir = document.UsersForm.dir;
      // 3. Alert_Box.js
      SetWrapper_Alert_Box(document.UsersForm.id);
      // 4. tbUsers_TabStripMenus_Operations.js
      GetBoxesHeaders_Users();
      // 5. tbUsers_TabStripMenus_Operations.js
      SetPosition_dropdowndive_Users();
      // 6. tbUsers_TabStripMenus_Operations.js
      ChangeEnabled_DropDownDive_Users('imgbox_SearchByPersonnel_Users', 'disabled');
      // 7. tbUsers_TabStripMenus_Operations.js
      SetActionMode_Users('View');
      // 8. tbUsers_TabStripMenus_Operations.js
      SetPageIndex_GridUsers_Users(0);
  }
 );
