cd C:\git\Microsoft.Xrm.DevOps.Data\Microsoft.Xrm.DevOps.Data.PowerShell\bin\Debug;
Import-Module .\Microsoft.Xrm.DevOps.Data.PowerShell.dll -Force;
$Conn = Get-CrmConnection -Interactive;

$json = ConvertFrom-Json -InputObject '{
      "solutionName": "Sym_Remote_Build",
      "fetches": [
        {
          "identifier": "po_optionitem",
          "fetch": "<fetch><entity name=\"po_optionitem\"><attribute name=\"statecode\"/><attribute name=\"po_label\"/><attribute name=\"bby_lineofbusinessid\"/><attribute name=\"po_optionitemid\"/><attribute name=\"po_optioncode\"/><attribute name=\"po_optionsconfigurationid\"/><attribute name=\"po_sortserialno\"/><attribute name=\"po_optionsconfigurationidname\"/><attribute name=\"po_sortorder\"/><attribute name=\"statuscode\"/><attribute name=\"po_value\"/><attribute name=\"bby_lineofbusinessidname\"/><filter><condition attribute=\"po_optionitemid\" operator=\"eq\" value=\"1DCE667F-FF77-E911-A96B-000D3A1D23D3\"/></filter></entity></fetch>"
        },
        {
          "identifier": "msdyusd_agentscriptaction",
          "fetch": "<fetch><entity name=\"msdyusd_agentscriptaction\" ><attribute name=\"msdyusd_agentscriptactionid\" /><link-entity name=\"msdyusd_subactioncalls\" from=\"msdyusd_agentscriptactionidtwo\" to=\"msdyusd_agentscriptactionid\" intersect=\"true\" ><attribute name=\"msdyusd_agentscriptactionidtwo\" /><link-entity name=\"msdyusd_agentscriptaction\" from=\"msdyusd_agentscriptactionid\" to=\"msdyusd_agentscriptactionidone\" ><attribute name=\"msdyusd_agentscriptactionid\" /></link-entity></link-entity></entity></fetch>"
        },
        {
          "identifier": "msdyusd_actioncallworkflow",
          "fetch": "<fetch><entity name=\"msdyusd_actioncallworkflow\" ><attribute name=\"msdyusd_name\" /><attribute name=\"msdyusd_actioncallworkflowid\" /><attribute name=\"msdyusd_workflow\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_answer",
          "fetch": "<fetch><entity name=\"msdyusd_answer\" ><attribute name=\"msdyusd_answerid\" /><link-entity name=\"msdyusd_answer_agentscriptaction\" from=\"msdyusd_answerid\" to=\"msdyusd_answerid\" intersect=\"true\" ><attribute name=\"msdyusd_agentscriptactionid\" /><link-entity name=\"msdyusd_agentscriptaction\" from=\"msdyusd_agentscriptactionid\" to=\"msdyusd_agentscriptactionid\" ><attribute name=\"msdyusd_agentscriptactionid\" /></link-entity></link-entity></entity></fetch>"
        },
        {
          "identifier": "msdyusd_task",
          "fetch": "<fetch><entity name=\"msdyusd_task\" ><attribute name=\"msdyusd_taskid\" /><link-entity name=\"msdyusd_task_agentscriptaction\" from=\"msdyusd_taskid\" to=\"msdyusd_taskid\" intersect=\"true\" ><attribute name=\"msdyusd_agentscriptactionid\" /><link-entity name=\"msdyusd_agentscriptaction\" from=\"msdyusd_agentscriptactionid\" to=\"msdyusd_agentscriptactionid\" ><attribute name=\"msdyusd_agentscriptactionid\" /></link-entity></link-entity><link-entity name=\"msdyusd_task_answer\" from=\"msdyusd_taskid\" to=\"msdyusd_taskid\" intersect=\"true\" ><attribute name=\"msdyusd_answerid\" /><link-entity name=\"msdyusd_answer\" from=\"msdyusd_answerid\" to=\"msdyusd_answerid\" ><attribute name=\"msdyusd_answerid\" /></link-entity></link-entity></entity></fetch>"
        },
        {
          "identifier": "msdyusd_agentscripttaskcategory",
          "fetch": "<fetch><entity name=\"msdyusd_agentscripttaskcategory\" ><attribute name=\"msdyusd_name\" /><attribute name=\"msdyusd_agentscripttaskcategoryid\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_configuration",
          "fetch": "<fetch><entity name=\"msdyusd_configuration\" ><attribute name=\"msdyusd_name\" /><attribute name=\"msdyusd_parentconfigurationidname\" /><attribute name=\"msdyusd_auditanddiagnosticssettingfieldname\" /><attribute name=\"msdyusd_auditanddiagnosticssettingfield\" /><attribute name=\"msdyusd_isdefaultname\" /><attribute name=\"msdyusd_parentconfigurationid\" /><attribute name=\"msdyusd_Description\" /><attribute name=\"msdyusd_configurationid\" /><attribute name=\"msdyusd_isdefault\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_search",
          "fetch": "<fetch><entity name=\"msdyusd_search\" ><attribute name=\"msdyusd_name\" /><attribute name=\"msdyusd_searchorder\" /><attribute name=\"msdyusd_fetchxml\" /><attribute name=\"msdyusd_searchid\" /><attribute name=\"msdyusd_directionname\" /><attribute name=\"msdyusd_direction\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_entitysearch",
          "fetch": "<fetch><entity name=\"msdyusd_entitysearch\" ><attribute name=\"msdyusd_name\" /><attribute name=\"msdyusd_entity\" /><attribute name=\"msdyusd_entityname\" /><attribute name=\"msdyusd_fetchxml\" /><attribute name=\"msdyusd_entitysearchid\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_entityassignment",
          "fetch": "<fetch><entity name=\"msdyusd_entityassignment\" ><attribute name=\"msdyusd_name\" /><attribute name=\"msdyusd_numericcode\" /><attribute name=\"msdyusd_entityassignmentid\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_uiievent",
          "fetch": "<fetch><entity name=\"msdyusd_uiievent\" ><attribute name=\"msdyusd_uiieventid\" /><link-entity name=\"msdyusd_uiievent_agentscriptaction\" from=\"msdyusd_uiieventid\" to=\"msdyusd_uiieventid\" intersect=\"true\" ><attribute name=\"msdyusd_agentscriptactionid\" /><link-entity name=\"msdyusd_agentscriptaction\" from=\"msdyusd_agentscriptactionid\" to=\"msdyusd_agentscriptactionid\" ><attribute name=\"msdyusd_agentscriptactionid\" /></link-entity></link-entity></entity></fetch>"
        },
        {
          "identifier": "msdyusd_form",
          "fetch": "<fetch><entity name=\"msdyusd_form\" ><attribute name=\"msdyusd_formid\" /><link-entity name=\"msdyusd_form_hostedapplication\" from=\"msdyusd_formid\" to=\"msdyusd_formid\" intersect=\"true\" ><attribute name=\"uii_hostedapplicationid\" /><link-entity name=\"uii_hostedapplication\" from=\"uii_hostedapplicationid\" to=\"uii_hostedapplicationid\" ><attribute name=\"uii_hostedapplicationid\" /></link-entity></link-entity></entity></fetch>"
        },
        {
          "identifier": "uii_hostedapplication",
          "fetch": "<fetch><entity name=\"uii_hostedapplication\" ><attribute name=\"uii_removesizingcontrolsname\" /><attribute name=\"msdyusd_allowmultiplepages\" /><attribute name=\"uii_extensionsxml\" /><attribute name=\"uii_isnomessagepump\" /><attribute name=\"msdyusd_prefetchdata\" /><attribute name=\"uii_managepopups\" /><attribute name=\"uii_isretainontaskbar\" /><attribute name=\"uii_name\" /><attribute name=\"uii_hostedapplicationtype\" /><attribute name=\"uii_externalappworkingdirectory\" /><attribute name=\"uii_icafilename\" /><attribute name=\"msdyusd_autolaunchname\" /><attribute name=\"uii_applicationhostingmode\" /><attribute name=\"uii_adapteruri\" /><attribute name=\"uii_adaptertype\" /><attribute name=\"uii_isautosignon\" /><attribute name=\"msdyusd_scanfordataparametersname\" /><attribute name=\"uii_usercanclosename\" /><attribute name=\"uii_isretainframeandcaptionname\" /><attribute name=\"msdyusd_savedurl\" /><attribute name=\"uii_iswebappusetoolbarname\" /><attribute name=\"uii_toplevelwindowmode\" /><attribute name=\"msdyusd_specifyurl\" /><attribute name=\"msdyusd_dashboardname\" /><attribute name=\"uii_minimumsizey\" /><attribute name=\"uii_isshowmenuname\" /><attribute name=\"msdyusd_xaml\" /><attribute name=\"msdyusd_paneltypename\" /><attribute name=\"uii_webappurl\" /><attribute name=\"uii_isappdynamicname\" /><attribute name=\"msdyusd_displayname\" /><attribute name=\"msdyusd_paneltype\" /><attribute name=\"uii_externalappuri\" /><attribute name=\"msdyusd_hostingtype\" /><attribute name=\"uii_iswebappusetoolbar\" /><attribute name=\"uii_isretainframeandcaption\" /><attribute name=\"uii_mainwindowacquisitiontimeout\" /><attribute name=\"uii_managepopupsname\" /><attribute name=\"msdyusd_hostingtypename\" /><attribute name=\"msdyusd_autolaunch\" /><attribute name=\"uii_isdependentonworkflow\" /><attribute name=\"uii_sortorder\" /><attribute name=\"uii_displaygroup\" /><attribute name=\"uii_isshowmenu\" /><attribute name=\"msdyusd_crmwindowhosttypename\" /><attribute name=\"uii_adaptermodename\" /><attribute name=\"msdyusd_crmwindowhosttype\" /><attribute name=\"uii_islimittoprocess\" /><attribute name=\"uii_isautosignonname\" /><attribute name=\"uii_automationxml\" /><attribute name=\"msdyusd_prefetchdataname\" /><attribute name=\"uii_isshowintoolbardropdownname\" /><attribute name=\"uii_isattachinputthread\" /><attribute name=\"uii_hostedapplicationid\" /><attribute name=\"uii_findwindowclass\" /><attribute name=\"uii_isusenewbrowserprocessname\" /><attribute name=\"uii_isglobalapplicationname\" /><attribute name=\"uii_islimittoprocessname\" /><attribute name=\"uii_applicationhostingmodename\" /><attribute name=\"uii_managehostingname\" /><attribute name=\"uii_assemblyuri\" /><attribute name=\"uii_usercanclose\" /><attribute name=\"msdyusd_maximumbrowsers\" /><attribute name=\"uii_isretainsystemmenu\" /><attribute name=\"uii_optimalsizex\" /><attribute name=\"uii_toplevelwindowmodename\" /><attribute name=\"uii_managehosting\" /><attribute name=\"uii_isrestoreifminimized\" /><attribute name=\"uii_isretainontaskbarname\" /><attribute name=\"uii_isdependentonworkflowname\" /><attribute name=\"uii_isglobalapplication\" /><attribute name=\"uii_removesizingcontrols\" /><attribute name=\"uii_remote_processacquisitionattempts\" /><attribute name=\"uii_isusenewbrowserprocess\" /><attribute name=\"msdyusd_scanfordataparameters\" /><attribute name=\"uii_isretainsystemmenuname\" /><attribute name=\"uii_assemblytype\" /><attribute name=\"uii_isappdynamic\" /><attribute name=\"uii_managedapplicationname\" /><attribute name=\"uii_isshowintoolbardropdown\" /><attribute name=\"uii_isattachinputthreadname\" /><attribute name=\"uii_adaptermode\" /><attribute name=\"uii_optimalsizey\" /><attribute name=\"uii_isnomessagepumpname\" /><attribute name=\"msdyusd_specifyurlname\" /><attribute name=\"uii_remote_processacquisitiondelay\" /><attribute name=\"msdyusd_allowmultiplepagesname\" /><attribute name=\"uii_hostedapplicationtypename\" /><attribute name=\"uii_processacquisitionfilename\" /><attribute name=\"uii_toplevelwindowcaption\" /><attribute name=\"uii_minimumsizex\" /><attribute name=\"uii_managedapplication\" /><attribute name=\"uii_isrestoreifminimizedname\" /><attribute name=\"uii_externalapparguments\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_languagemodule",
          "fetch": "<fetch><entity name=\"msdyusd_languagemodule\" ><attribute name=\"msdyusd_location\" /><attribute name=\"msdyusd_name\" /><attribute name=\"msdyusd_lcid\" /><attribute name=\"msdyusd_globalmanageridname\" /><attribute name=\"msdyusd_languagemoduleid\" /><attribute name=\"msdyusd_languagecode\" /><attribute name=\"msdyusd_globalmanagerid\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_scripttasktrigger",
          "fetch": "<fetch><entity name=\"msdyusd_scripttasktrigger\" ><attribute name=\"msdyusd_name\" /><attribute name=\"msdyusd_scripttasktriggertype\" /><attribute name=\"msdyusd_scripttasktriggerid\" /><attribute name=\"msdyusd_scripttasktriggertypename\" /><attribute name=\"msdyusd_scripttasktriggerdata\" /><attribute name=\"msdyusd_triggeridname\" /><attribute name=\"msdyusd_triggerid\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_scriptlet",
          "fetch": "<fetch><entity name=\"msdyusd_scriptlet\" ><attribute name=\"msdyusd_name\" /><attribute name=\"msdyusd_scriptletid\" /><attribute name=\"msdyusd_scripttext\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_sessioninformation",
          "fetch": "<fetch><entity name=\"msdyusd_sessioninformation\" ><attribute name=\"msdyusd_order\" /><attribute name=\"msdyusd_name\" /><attribute name=\"msdyusd_typename\" /><attribute name=\"msdyusd_display\" /><attribute name=\"msdyusd_type\" /><attribute name=\"msdyusd_selectedentity\" /><attribute name=\"msdyusd_selectedentityname\" /><attribute name=\"msdyusd_sessioninformationid\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_sessiontransfer",
          "fetch": "<fetch><entity name=\"msdyusd_sessiontransfer\" ><attribute name=\"msdyusd_name\" /><attribute name=\"msdyusd_sessiontransferid\" /><attribute name=\"msdyusd_sessioninformation\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_toolbarstrip",
          "fetch": "<fetch><entity name=\"msdyusd_toolbarstrip\" ><attribute name=\"msdyusd_toolbarstripid\" /><link-entity name=\"msdyusd_toolbarstrip_uii_hostedapplication\" from=\"msdyusd_toolbarstripid\" to=\"msdyusd_toolbarstripid\" intersect=\"true\" ><attribute name=\"uii_hostedapplicationid\" /><link-entity name=\"uii_hostedapplication\" from=\"uii_hostedapplicationid\" to=\"uii_hostedapplicationid\" ><attribute name=\"uii_hostedapplicationid\" /></link-entity></link-entity></entity></fetch>"
        },
        {
          "identifier": "msdyusd_toolbarbutton",
          "fetch": "<fetch><entity name=\"msdyusd_toolbarbutton\" ><attribute name=\"msdyusd_toolbarbuttonid\" /><link-entity name=\"msdyusd_toolbarbutton_agentscriptaction\" from=\"msdyusd_toolbarbuttonid\" to=\"msdyusd_toolbarbuttonid\" intersect=\"true\" ><attribute name=\"msdyusd_agentscriptactionid\" /><link-entity name=\"msdyusd_agentscriptaction\" from=\"msdyusd_agentscriptactionid\" to=\"msdyusd_agentscriptactionid\" ><attribute name=\"msdyusd_agentscriptactionid\" /></link-entity></link-entity></entity></fetch>"
        },
        {
          "identifier": "uii_action",
          "fetch": "<fetch><entity name=\"uii_action\" ><attribute name=\"uii_isrunmodeasynchronous\" /><attribute name=\"msdyusd_unifiedservicedeskcreatedname\" /><attribute name=\"uii_scriptfilepathtorun\" /><attribute name=\"uii_hostedapplicationidname\" /><attribute name=\"uii_name\" /><attribute name=\"uii_isfocussedapplicationname\" /><attribute name=\"msdyusd_unifiedservicedeskcreated\" /><attribute name=\"msdyusd_help\" /><attribute name=\"uii_querystring\" /><attribute name=\"uii_automationmodename\" /><attribute name=\"uii_isfocussedapplication\" /><attribute name=\"uii_workflowxaml\" /><attribute name=\"uii_workflowrules\" /><attribute name=\"uii_isdefault\" /><attribute name=\"uii_actionid\" /><attribute name=\"uii_automationmode\" /><attribute name=\"uii_methodname\" /><attribute name=\"uii_isdefaultname\" /><attribute name=\"uii_url\" /><attribute name=\"uii_isrunmodeasynchronousname\" /><attribute name=\"uii_workflowassemblytype\" /><attribute name=\"uii_extensionsxml\" /><attribute name=\"uii_hostedapplicationid\" /><attribute name=\"uii_method\" /></entity></fetch>"
        },
        {
          "identifier": "msdyusd_windowroute",
          "fetch": "<fetch><entity name=\"msdyusd_windowroute\" ><attribute name=\"msdyusd_windowrouteid\" /><link-entity name=\"msdyusd_windowroute_agentscriptaction\" from=\"msdyusd_windowrouteid\" to=\"msdyusd_windowrouteid\" intersect=\"true\" ><attribute name=\"msdyusd_agentscriptactionid\" /><link-entity name=\"msdyusd_agentscriptaction\" from=\"msdyusd_agentscriptactionid\" to=\"msdyusd_agentscriptactionid\" ><attribute name=\"msdyusd_agentscriptactionid\" /></link-entity></link-entity><link-entity name=\"msdyusd_windowroute_ctisearch\" from=\"msdyusd_windowrouteid\" to=\"msdyusd_windowrouteid\" intersect=\"true\" ><attribute name=\"msdyusd_searchid\" /><link-entity name=\"msdyusd_search\" from=\"msdyusd_searchid\" to=\"msdyusd_searchid\" ><attribute name=\"msdyusd_searchid\" /></link-entity></link-entity></entity></fetch>"
        },
        {
          "identifier": "msdyusd_customizationfiles",
          "fetch": "<fetch><entity name=\"msdyusd_customizationfiles\"><link-entity name=\"msdyusd_customizationfiles_configuration\" from=\"msdyusd_customizationfilesid\" to=\"msdyusd_customizationfilesid\" intersect=\"true\"><attribute name=\"msdyusd_configurationid\"/><link-entity name=\"msdyusd_configuration\" from=\"msdyusd_configurationid\" to=\"msdyusd_configurationid\"><attribute name=\"msdyusd_configurationid\"/></link-entity></link-entity></entity></fetch>"
        },
        {
          "identifier": "msdyusd_auditanddiagnosticssetting",
          "fetch": "<fetch><entity name=\"msdyusd_auditanddiagnosticssetting\" ><attribute name=\"msdyusd_auditanddiagnosticssettingid\" /><link-entity name=\"msdyusd_auditdiag_tracesourcesetting\" from=\"msdyusd_auditanddiagnosticssettingid\" to=\"msdyusd_auditanddiagnosticssettingid\" intersect=\"true\" ><attribute name=\"msdyusd_tracesourcesettingid\" /><link-entity name=\"msdyusd_tracesourcesetting\" from=\"msdyusd_tracesourcesettingid\" to=\"msdyusd_tracesourcesettingid\" ><attribute name=\"msdyusd_tracesourcesettingid\" /></link-entity></link-entity></entity></fetch>"
        },
        {
          "identifier": "annotation",
          "fetch": "<fetch><entity name=\"annotation\"><attribute name=\"annotationid\"/><attribute name=\"documentbody\"/><attribute name=\"filename\"/><attribute name=\"filesize\"/><attribute name=\"isdocument\"/><attribute name=\"langid\"/><attribute name=\"mimetype\"/><attribute name=\"notetext\"/><attribute name=\"objectid\"/><attribute name=\"subject\"/><filter type=\"and\"><condition entityname=\"customizationfiles\" attribute=\"msdyusd_customizationfilesid\" operator=\"not-null\"/></filter><link-entity name=\"msdyusd_customizationfiles\" from=\"msdyusd_customizationfilesid\" to=\"objectid\" alias=\"customizationfiles\"/></entity></fetch>"
        }
      ]
    }';
Get-CrmDataPackage -Conn $Conn -Fetches $json.fetches.fetch -Verbose;



#$firstPackage = Get-CrmDataPackage -Conn $Conn -Fetches @("<fetch><entity name='account'><all-attributes/></entity></fetch>") -Verbose;
#Get-CrmDataPackage -Conn $Conn -Fetches @("<fetch><entity name='contact'><all-attributes/></entity></fetch>", "<fetch><entity name='category'><all-attributes/></entity></fetch>") -Identifiers @{ "contact" = @("firstname", "lastname"); "category" = @("categoryid") } -DisablePlugins @{ "contact" = $true } -DisablePluginsGlobally $true -Verbose `
#    | Add-FetchesToCrmDataPackage -Conn $Conn -Fetches @("<fetch><entity name='knowledgearticle'><all-attributes/></entity></fetch>") -Verbose `
#    | Merge-CrmDataPackage -AdditionalPackage $firstPackage -Verbose `
#	| Remove-CrmDataPackage -RemovePackage $firstPackage -Verbose `
#    | Export-CrmDataPackage -ZipPath $env:USERPROFILE\Downloads\testrun.zip -Verbose