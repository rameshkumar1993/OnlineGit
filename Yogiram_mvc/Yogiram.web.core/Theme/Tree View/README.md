# jquery.checkTree 

        <div id="tree-container"></div>
 
    <script>
            var mockData = [];
            mockData.push({
                item:{
                    id: 'id1',
                    label: 'Lorem ipsum dolor 1',
                    checked: false
                },
                children: [{
                   item:{
                        id: 'id11',
                        label: 'Lorem ipsum dolor 11',
                        checked: false
                    } 
                },{
                   item:{
                        id: 'id12',
                        label: 'Lorem ipsum dolor 12',
                        checked: false
                    } 
                },{
                   item:{
                        id: 'id13',
                        label: 'Lorem ipsum dolor 13',
                        checked: false
                    } 
                }]
            });

            mockData.push({
                item:{
                    id: 'id2',
                    label: 'Lorem ipsum dolor 2',
                    checked: false
                },
                children: [{
                   item:{
                        id: 'id21',
                        label: 'Lorem ipsum dolor 21',
                        checked: false
                    } 
                },{
                   item:{
                        id: 'id22',
                        label: 'Lorem ipsum dolor 22',
                        checked: true
                    } 
                },{
                   item:{
                        id: 'id23',
                        label: 'Lorem ipsum dolor 23',
                        checked: false
                    } 
                }]
            });

            mockData.push({
                item:{
                    id: 'id3',
                    label: 'Lorem ipsum dolor 3',
                    checked: false
                },
                children: [{
                   item:{
                        id: 'id31',
                        label: 'Lorem ipsum dolor 31',
                        checked: true
                    } 
                },{
                   item:{
                        id: 'id32',
                        label: 'Lorem ipsum dolor 32',
                        checked: false
                    },
                    children: [{
                        item:{
                            id: 'id321',
                            label: 'Lorem ipsum dolor 321',
                            checked: false
                        }
                    },{
                        item:{
                            id: 'id322',
                            label: 'Lorem ipsum dolor 322',
                            checked: false
                        }
                    }]
                }]
            });

            $('#tree-container').highCheckTree({
                data: mockData
            });
        </script> 
