function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 10,
        center: {
            lat: 51.571095,
            lng: -0.935898
        }
    });

    // request list of stores from web api
    var request = new XMLHttpRequest();
    request.open('GET', '/track', true);

    request.onload = function () {
        if (request.status >= 200 && request.status < 400) {
            // Success!
            var track = JSON.parse(request.responseText);
            var evnts = track.events;
            document.title = track.name;
            document.getElementById('title').innerText = track.name;

            var points = [];
            for (var i = 0; i < evnts.length; i++) {
                var evnt = evnts[i];
                var latLng = new google.maps.LatLng(evnt.latitude, evnt.longitude);
                points.push(latLng)
                if (i == 0) { map.setCenter(latLng); }
                if (evnt.accuracy) {
                    var circle = new google.maps.Circle({
                        strokeColor: 'blue',
                        strokeOpacity: 0.5,
                        fillColor: 'blue',
                        fillOpacity: 0.35,
                        map: map,
                        center: latLng,
                        radius: evnt.accuracy
                    });
                }

                var marker = new google.maps.Marker({
                    position: latLng,
                    map: map,
                    title: evnt.eventTime
                });
            }

            var path = new google.maps.Polyline({
                path: points,
                strokeColor: "black",
                strokeOpacity: 0.5,
                strokeWeight: 1,
                map: map
            });
        }
        else {
            document.getElementById('title').innerText = 'No tracks found';
        }
    };
    request.send();
}