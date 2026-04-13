import { useEffect, useState } from "react";
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import Navbar from "../components/Navbar";

function MapPage() {
  const [parkingData, setParkingData] = useState<any[]>([]);
  const [startTime, setStartTime] = useState("2030-01-01T10:00");
  const [endTime, setEndTime] = useState("2030-01-01T12:00");

  const [userLocation, setUserLocation] = useState({
    lat: 17.385,
    lng: 78.486,
  });

  const token = localStorage.getItem("token");

  // 📍 Get current location
  useEffect(() => {
    navigator.geolocation.getCurrentPosition(
      (pos) => {
        setUserLocation({
          lat: pos.coords.latitude,
          lng: pos.coords.longitude,
        });
      },
      (err) => console.error(err)
    );
  }, []);

  // 🔥 Fetch parking data
  const fetchData = () => {
    fetch(
      `http://localhost:5019/api/ParkingSpaces/nearby-available?lat=${userLocation.lat}&lng=${userLocation.lng}&startTime=${startTime}&endTime=${endTime}`
    )
      .then((res) => res.json())
      .then((data) => {
        const sorted = data.sort(
          (a: any, b: any) => a.distance - b.distance
        );
        setParkingData(sorted);
      });
  };

  useEffect(() => {
    fetchData();
  }, [userLocation]);

  // 🚗 BOOK SLOT
  const handleBooking = async (slotId: string) => {
    const res = await fetch("http://localhost:5019/api/Bookings", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        slotId,
        startTime,
        endTime,
      }),
    });

    if (res.ok) {
      alert("Booking successful ✅");
      fetchData();
    } else {
      const error = await res.text();
      alert(error);
    }
  };

  return (
    <div style={{ display: "flex", flexDirection: "column", height: "100vh",  }}>
      
      {/* ✅ NAVBAR */}
      <Navbar style="border: red;"/>

      {/* 🔍 FILTER */}
      <div style={{ padding: "10px", background: "#fff" }}>
        <input
          type="datetime-local"
          value={startTime}
          onChange={(e) => setStartTime(e.target.value)}
        />

        <input
          type="datetime-local"
          value={endTime}
          onChange={(e) => setEndTime(e.target.value)}
          style={{ marginLeft: "10px" }}
        />

        <button onClick={fetchData} style={{ marginLeft: "10px" }}>
          Search
        </button>
      </div>

      {/* 🗺 MAP */}
      <div style={{ flex: 1 }}>
        <MapContainer
          key={userLocation.lat}
          center={[userLocation.lat, userLocation.lng]}
          zoom={13}
          style={{ height: "100%", width: "100%" }}
        >
          <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />

          {/* 📍 USER */}
          <Marker position={[userLocation.lat, userLocation.lng]}>
            <Popup>You are here 📍</Popup>
          </Marker>

          {/* 🚗 PARKING */}
          {parkingData.map((p: any) => (
            <Marker key={p.id} position={[p.latitude, p.longitude]}>
              <Popup>
                <b>{p.title}</b>
                <br />
                {p.address}
                <br />
                📍 {p.distance.toFixed(2)} km

                <hr />

                {p.availableSlots.map((slot: any) => (
                  <div key={slot.id}>
                    {slot.slotNumber} - ₹{slot.pricePerHour}
                    <br />
                    <button onClick={() => handleBooking(slot.id)}>
                      Book
                    </button>
                  </div>
                ))}
              </Popup>
            </Marker>
          ))}
        </MapContainer>
      </div>
    </div>
  );
}

export default MapPage;