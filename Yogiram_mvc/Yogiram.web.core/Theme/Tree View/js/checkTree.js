/**
 *name: jquery.checkTree
 *author: Elyor Latipov
 *version: 0.1.1
*/

/**input data format: 

[{
    item:{id:'id', label:'label',value:1, Checked:false}, 
    chidren:[{
        item:{id:'id', label:'label', Checked:false}, 
        chidren:[...]
    }]
}, ....]

*/

(function ($) {

    jQuery.fn.checkTree = function (settings) {

        settings = $.extend({
            data: [],               // input data which will be used to initilze the tree
            onExpand: null,        // an event will be triggered when the tree node was expanded
            onCollapse: null,      // an event will be triigered when the tree node was collapsed
            onPreCheck: null,      // an event will be triggered before the tree node was checked
            onCheck: null,         // an event will be triggered when the tree node was checked
            onUnCheck: null,       // an event will be triggered when the tree node was unchecked
            onLabelHoverOver: null,// an event will be triggered when mouse hover over the label
            onLabelHoverOut: null  // an event will be triggered when mouse hover out the label
        }, settings);

        var container = $(this), $tree = this;

        //get children html tag string
        function getChildrenHtml(treesdata) {
            var result = '', len = treesdata.length, node, clen, arrowClass, checkedClass = ''
            checkedChildren;
            for (i = 0; i < len; i++) {
                node = treesdata[i];
                $.data($tree, node.item.id, node); //attach node data to node id
                clen = node.children ? node.children.length : 0;
                arrowClass = 'collapsed';
                if (clen === 0) {
                    arrowClass = 'nochildren';
                    checkClass = node.item.Checked ? 'checked' : '';
                } else {
                    var checkedChildren = $.grep(node.children, function (el) {
                        return el.item.Checked;
                    });
                    checkClass = checkedChildren.length === 0 ? '' : checkedChildren.length === clen ? 'checked' : 'half_checked';
                }

                result += '<li rel="' + node.item.id + '"><div class="arrow ' + arrowClass + '"></div><div class="checkbox ' + checkClass + '"><input type="hidden" value="'+node.item.value+'"/></div><label>' +
                             node.item.label + '</label></li>';
            }

            return result;
        }

        //display children node with data source
        function updateChildrenNodes($li, data, isExpanded) {
            if (data.children && data.children.length > 0) {
                var innerHtml = isExpanded ? '<ul>' : '<ul style="display:none;">';
                innerHtml += getChildrenHtml(data.children) + '</ul>';
                $li.append(innerHtml);
            }

            $li.addClass('updated');
        }

        //initialize the check tree with the input data
        (function initalCheckTree() {
            var treesHtml = '<ul class="checktree">';
            treesHtml += getChildrenHtml(settings.data);
            container.empty().append(treesHtml + '<ul>');
        })();

        //bind select change to checkbox
        container.off('selectchange', '.checkbox').on('selectchange', '.checkbox', function () {
            if (settings.onPreCheck) {
                if (!settings.onPreCheck($(this).parent())) {
                    return;
                }
            }

            var $li = $(this).parent();
            var dataSource = $.data($tree, $li.attr('rel'));
            var $all = $(this).siblings('ul').find('.checkbox');
            var $checked = $all.filter('.checked');

            //all children checked
            if ($all.length === $checked.length) {
                $(this).removeClass('half_checked').addClass('checked');
                dataSource.item.Checked = true;
                if (settings.onCheck) {
                    settings.onCheck($li);
                }
                //all children are unchecked
            } else if ($checked.length === 0) {
                dataSource.item.Checked = false;
                $(this).removeClass('checked').removeClass('half_checked');
                if (settings.onUnCheck) {
                    settings.onUnCheck($li);
                }
                //some children are checked
            } else {
                dataSource.item.Checked = false;
                if (settings.onHalfCheck && !$(this).hasClass('half_checked')) {
                    settings.onHalfCheck($li);
                }

                $(this).removeClass('checked').addClass('half_checked');
            }
        });

        //expand and collapse node
        container.off('click', '.arrow').on('click', '.arrow', function () {
            if ($(this).hasClass('nochildren')) {
                return;
            }

            var $li = $(this).parent();
            if (!$li.hasClass('updated')) {
                updateChildrenNodes($li, $.data($tree, $li.attr('rel')), true);
                
                if (settings.onExpand) {
                    settings.onExpand($li);
                }
                $(this).removeClass('collapsed').addClass('expanded');
            } else {
                $(this).siblings("ul").toggle();
                if ($(this).hasClass('collapsed')) {
                    if (settings.onExpand) {
                        settings.onExpand($li);
                    }
                    $(this).removeClass('collapsed').addClass('expanded');

                } else {
                   
                    if (settings.onCollapse) {
                        settings.onCollapse($li);
                    }
                    $(this).removeClass('expanded').addClass('collapsed');
                }
            }
        });

        //check and uncheck node
        container.off('click', '.checkbox').on('click', '.checkbox', function () {
            var $li = $(this).parent();
            var dataSource = $.data($tree, $li.attr('rel'));
            if (!$li.hasClass('updated')) {
                updateChildrenNodes($li, dataSource, false);
            }

            if (settings.onPreCheck) {
                if (!settings.onPreCheck($li)) {
                    return;
                }
            }

            $(this).removeClass('half_checked').toggleClass('checked');

            if ($(this).hasClass('checked')) {
                dataSource.item.Checked = true;
                if (settings.onCheck) {
                    settings.onCheck($li, true);
                }

                $(this).siblings('ul').find('.checkbox').not('.checked').removeClass('half_checked').addClass('checked').each(function () {
                    var $subli = $(this).parent();
                    $.data($tree, $subli.attr('rel')).item.Checked = true;
                    if (settings.onCheck) {
                        settings.onCheck($subli, false);
                    }
                });
            } else {
                dataSource.item.Checked = false;
                if (settings.onUnCheck) {
                    settings.onUnCheck($li, true);
                }

                $(this).siblings('ul').find('.checkbox').filter('.checked').removeClass('half_checked').removeClass('checked').each(function () {
                    var $subli = $(this).parent();
                    $.data($tree, $subli.attr('rel')).item.Checked = false;
                    if (settings.onUnCheck) {
                        settings.onUnCheck($subli, false);
                    }
                });
            }

            $(this).parents('ul').siblings('.checkbox').trigger('selectchange');
        });

        //click label also trigger check action
        container.off('click', 'label').on('click', 'label', function () {
            $(this).prev('.checkbox').trigger('click');
        });

        container.off('mouseenter', 'label').on('mouseenter', 'label', function () {
            $(this).addClass("hover");
            if (settings.onLabelHoverOver) settings.onLabelHoverOver($(this).parent());
        });

        container.off('mouseleave', 'label').on('mouseleave', 'label', function () {
            $(this).removeClass("hover");
            if (settings.onLabelHoverOut) settings.onLabelHoverOut($(this).parent());
        });

    };
})(jQuery);
