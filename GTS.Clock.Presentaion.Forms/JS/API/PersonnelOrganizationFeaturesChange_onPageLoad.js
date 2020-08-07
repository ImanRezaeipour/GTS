

$(document).ready
        (
            function () {
                parent.DialogLoading.Close();
                document.body.dir = document.PersonnelOrganizationFeaturesChangeForm.dir;
                SetWrapper_Alert_Box(document.PersonnelOrganizationFeaturesChangeForm.id);
                //ViewCurrentLangCalendars_PersonnelOrganizationFeaturesChange();
                ResetCalendars_PersonnelOrganizationFeaturesChange();
                SetPosition_PersonnelOrganizationFeatures();
                SetPosition_cmbPersonnel_PersonnelOrganizationFeaturesChange();
                GetBoxesHeaders_PersonnelOrganizationFeatures();
                SetPersonnelCountHeader_PersonnelOrganizationFeaturesChange('Load');
            }
        );
