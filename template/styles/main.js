var years = ["2017", "2016", "2015", "2014", "2013"];
var tags = ["Library", "Tools", "Graphics", "Interactive"];
var stats = ["Active", "In Progress", "Archived", "Deprecated", "Abandoned"];

$(function() {

    // Generate buttons from arrays above

    for (var i = 0; i < tags.length; i++) {
        var r = $('<button class="tgl" id="tgl-' + tags[i] + '" title="Filter by project type: ' + tags[i] +'" onclick="goHideTag(\'' + tags[i] + '\')">' + tags[i]+'</button>');
        $("#tag-selector").append(r);
    }

     for (var i = 0; i < stats.length; i++) {
        var r = $('<button class="sgl" id="sgl-' + i + '" title="Filter by project status: ' + stats[i] +'" onclick="goHideStat(\'' + i + '\')">' + stats[i]+'</button>');
        $("#stat-selector").append(r);
    }

    for (var i = 0; i < years.length; i++) {
        var r = $('<button title="Goto projects created in year: ' + years[i] +'" onclick="goScroll(\'' + years[i] + '\')">' + years[i]+'</button>');
        $("#year-selector").append(r);
    }

    /// Generate count
    $(".counting").text($(".projects").length);
});

function goHideTag(tag)
{
    // turn off all sgl first
    $(".sgl").each(function (index, element) {
          if ($(this).hasClass("shadow"))
            goHideStat($(this).attr('id').substring(4));
    });
    $('.t-' +  tag).toggle(500);
    $('#tgl-' + tag).toggleClass("shadow");
}

function goHideStat(stat)
{
    // turn off all tgl first
    $(".tgl").each(function (index, element) {
          if ($(this).hasClass("shadow"))
            goHideTag($(this).attr('id').substring(4));
    });
    $('.s-' +  stat).toggle(500);
    $('#sgl-' + stat).toggleClass("shadow");
}

function goScroll(href)
{
    // Smooth scroll
    $('html, body').animate({
            scrollTop: $('#' + href).offset().top
    }, 500);
}