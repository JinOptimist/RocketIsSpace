ymaps.ready(init);
var myMap;

function init() {
    myMap = new ymaps.Map("map", {
        center: [53.925269, 27.508175],
        zoom: 16

    });
    myMap.controls
        .remove('geolocationControl')
        .remove('trafficControl')
        .remove('rulerControl')
        .remove('searchControl');

    myMap.behaviors.disable([
        'drag',
        'scrollZoom'
    ]);

    myPlacemark = new ymaps.Placemark([53.925269, 27.508175], {
        balloonContentHeader: '����',
        balloonContentBody: '����� �������',
        balloonContentFooter: '����� ����������� �����',
        hintContent: '��� �� �������'
    });
    myMap.geoObjects.add(myPlacemark);
}