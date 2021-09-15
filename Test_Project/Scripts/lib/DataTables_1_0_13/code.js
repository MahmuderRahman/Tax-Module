<!-- Data Table  -->
// <script type="text/javascript" src="<?php echo base_url();?>js/DataTables-1.10.13/media/js/jquery.dataTables.min.js"></script>
// <script type="text/javascript" src="<?php echo base_url();?>js/DataTables-1.10.13/media/js/dataTables.bootstrap.min.js"></script>

// <script type="text/javascript" src="<?php echo base_url();?>js/DataTables-1.10.13/extensions/FixedColumns/js/dataTables.fixedColumns.min.js"></script>      

// <script type="text/javascript" src="<?php echo base_url();?>js/DataTables-1.10.13/media/js/dataTables.rowsGroup.js"></script>       

// <script type="text/javascript" src="<?php echo base_url();?>js/DataTables-1.10.13/extensions/Buttons/js/dataTables.buttons.min.js"></script>
// <script type="text/javascript" src="<?php echo base_url();?>js/DataTables-1.10.13/extensions/Buttons/js/buttons.bootstrap.min.js"></script>
// <script type="text/javascript" src="<?php echo base_url();?>js/DataTables-1.10.13/extensions/Export/jszip.min.js"></script>
// <script type="text/javascript" src="<?php echo base_url();?>js/DataTables-1.10.13/extensions/Export/pdfmake.min.js"></script>
// <script type="text/javascript" src="<?php echo base_url();?>js/DataTables-1.10.13/extensions/Export/vfs_fonts.js"></script>
// <script type="text/javascript" src="<?php echo base_url();?>js/DataTables-1.10.13/extensions/Buttons/js/buttons.html5.min.js"></script>
// <script type="text/javascript" src="<?php echo base_url();?>js/DataTables-1.10.13/extensions/Buttons/js/buttons.print.min.js"></script>
        

// <link rel="stylesheet" type="text/css" href="<?php echo base_url();?>js/DataTables-1.10.13/media/css/dataTables.bootstrap.min.css">
// <link rel="stylesheet" type="text/css" href="<?php echo base_url();?>js/DataTables-1.10.13/extensions/FixedColumns/css/fixedColumns.bootstrap.min.css">
// <link rel="stylesheet" type="text/css" href="<?php echo base_url();?>js/DataTables-1.10.13/extensions/Buttons/css/buttons.dataTables.min.css">



exDataTable = $('#exDataTable').DataTable({
                dom: 'lB<"toolbar">frtip',         
                buttons: [                               
                {
                    extend: 'excelHtml5',
                    exportOptions: {
                     columns: [4,5,6,7,8,9,10,11,12,13,14,15,16,17]
                    },
                    title: 'Student Activity <?php echo $assEnroll['enrollInfo']->username;?>_<?php echo $assEnroll['enrollInfo']->id;?>'
                }
                ], 
                scrollY:        "500",
                scrollX:        true,
                scrollCollapse: true,            
                lengthMenu : [[100, 250, 500, -1], [100, 250, 500, "All"]],
                columnDefs : [                              
                                    {   visible: false,  targets: [0,1,2,3] },
                ],
                columns : [
                                { data : 'id', name : 'id', title: 'id'},                                
                                { data : 'aId', name : 'aId', title: 'ACT ID'},
                                { data : 'asuName', name : 'asuName', title: 'ACT U N'},
                                { data : 'session_day', name : 'session_day', title: 'ACT T P'},                                
                                { data : 'problem_name', name : 'problem_name', title: 'Problem', render : function(data, type, row){
                                                                                                        return utlt.fId(row['id'],'PROB')+'.'+data;
                                                                                                }
                                },
                                { data : 'pHML', name : 'pHML', title: 'PROB. HML'},
                                { data : 'pp', name : 'pp', title: 'PP', class: 'assPP'},
                                { data : 'remarks', name : 'remarks', class: 'assRemarks', title: 'Ass. Remarks'},
                                { data : 'activity_name', name : 'activity_name', title: 'ACT. Name', render : function(data, type, row){
                                                                                                        return utlt.fId(row['aId'],'ACT')+'.'+data;
                                                                                                    }
                                },
                                { data : 'aDept', name : 'aDept', title: 'ACT DEPT'},
                                { data : 'activityType', name : 'activityType', title: 'ACT. Type'},                                
                                { data : 'asuT', name : 'asuT', title: 'ASUT'},
                                { data : 'ASU', name : 'ASU', title: 'AU Number/ Session', render : function(data, type, row){
                                                                                        return data+' '+row['asuName'].split("|")[0];
                                                                                    }
                                },
                                { data : 'AST', name : 'AST', title: 'AU Duraion/ Session', render : function(data, type, row){
                                                                                        return data+' '+row['asuName'].split("|")[1];
                                                                                    }
                                },                                
                                { data : 'total_session', name : 'total_session', title: 'Total Session/ Days', render : function(data, type, row){
                                                                                                                    return data + '/' + row['session_day']+'D';
                                                                                                                }
                                },
                                { data : 'session_home', name : 'session_home', title: 'ACT Home'},
                                { data : 'session_school', name : 'session_school', title: 'ACT School'},
                                { data : 'paRemarks', name : 'paRemarks', title: 'Remarks'},
                                { name : 'OPT', title: 'OPT', render : function(){return '<span class="setACTPlan glyphicon glyphicon-edit text-info cP" title="Remarks"></span>';}
                                }
                ],
                fixedColumns : true,
                data : allPrbList,
                rowsGroup : ['problem_name:name','pHML:name', 'pp:name','remarks:name']
            });