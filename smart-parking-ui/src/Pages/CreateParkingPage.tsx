import { useState, useEffect } from "react";
import Navbar from "../components/Navbar";
import {
  MapContainer,
  TileLayer,
  Marker,
  useMap,
  useMapEvents,
} from "react-leaflet";
import "leaflet/dist/leaflet.css";
import L from "leaflet";

// 🔥 FIX MARKER ICON ISSUE (VERY IMPORTANT)
delete (L.Icon.Default.prototype as any)._getIconUrl;

L.Icon.Default.mergeOptions({
  iconRetinaUrl:
    "https://unpkg.com/leaflet@1.7.1/dist/images/marker-icon-2x.png",
  iconUrl:
    "https://unpkg.com/leaflet@1.7.1/dist/images/marker-icon.png",
  shadowUrl:
    "https://unpkg.com/leaflet@1.7.1/dist/images/marker-shadow.png",
});

// 📍 CLICK HANDLER
function LocationSelector({ setLatLng }: any) {
  useMapEvents({
    click(e) {
      setLatLng({
        lat: e.latlng.lat,
        lng: e.latlng.lng,
      });
    },
  });
  return null;
}

// 🔥 FORCE MAP TO RECENTER
function RecenterMap({ latLng }: any) {
  const map = useMap();

  useEffect(() => {
    if (latLng) {
      map.setView([latLng.lat, latLng.lng], 15);
    }
  }, [latLng]);

  return null;
}

function CreateParkingPage() {
  const [title, setTitle] = useState("");
  const [address, setAddress] = useState("");
  const [latLng, setLatLng] = useState<any>(null);
  const [pricePerHour, setPricePerHour] = useState("");
  const [pricePerDay, setPricePerDay] = useState("");
  const token = localStorage.getItem("token");

  // 🔥 AUTO LOCATION DETECT
  useEffect(() => {
    navigator.geolocation.getCurrentPosition(
      (pos) => {
        const location = {
          lat: pos.coords.latitude,
          lng: pos.coords.longitude,
        };

        console.log("📍 Auto location:", location);

        setLatLng(location);
      },
      (err) => {
        console.error("Location error:", err);
      }
    );
  }, []);

  const handleCreate = async () => {
    if (!latLng) {
      alert("Please select location");
      return;
    }

    const res = await fetch("http://localhost:5019/api/ParkingSpaces", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        title,
        address,
        latitude: latLng.lat,
        longitude: latLng.lng,
        pricePerHour: parseFloat(pricePerHour),
        pricePerDay: pricePerDay ? parseFloat(pricePerDay) : null,
      }),
    });

    if (res.ok) {
      alert("Parking created ✅");
    } else {
      alert("Failed ❌");
    }
  };

  return (
    <div style={{ display: "flex", flexDirection: "column", height: "100vh" }}>
      
      {/* 🔝 NAVBAR */}
      <Navbar />

      {/* 📝 FORM */}
      <div style={{ padding: "15px", background: "#fff" }}>
        <h2>Create Parking</h2>

        <input
          placeholder="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          style={{ marginRight: "10px", padding: "8px" }}
        />

        <input
          placeholder="Address"
          value={address}
          onChange={(e) => setAddress(e.target.value)}
          style={{ marginRight: "10px", padding: "8px" }}
        />
<input placeholder="Price Per Hour" value={pricePerHour} onChange={(e) => setPricePerHour(e.target.value)}
/>
        <button onClick={handleCreate}>Create</button>

        {latLng && (
          <p>
            📍 {latLng.lat.toFixed(5)}, {latLng.lng.toFixed(5)}
          </p>
        )}
      </div>

      {/* 🗺 MAP */}
      <div style={{ flex: 1 }}>
        <MapContainer
          center={
            latLng
              ? [latLng.lat, latLng.lng]
              : [17.385, 78.486]
          }
          zoom={15}
          style={{ height: "100%", width: "100%" }}
        >
          <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />

          {/* 🔥 RECENTER */}
          <RecenterMap latLng={latLng} />

          {/* 🔥 CLICK SELECT */}
          <LocationSelector setLatLng={setLatLng} />

          {/* 🔥 MARKER */}
          {latLng && <Marker position={[latLng.lat, latLng.lng]} />}
        </MapContainer>
      </div>
    </div>
  );
}

export default CreateParkingPage;