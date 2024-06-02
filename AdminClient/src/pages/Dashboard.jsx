import React, { useEffect, useState } from "react";
import Header from "../components/Header";
import ClientData from "../components/ClientData";
import { useSelector } from "react-redux";
import ReactJson from "react-json-view";

const Dashboard = () => {
  const temp = {
    name: "Ankush",
    hobby: "Computer",
  };

  const [dashData, setDashData] = useState([]);

  const getDashboardData = async () => {
    const res = await fetch("https://localhost:5001/AdminApi/GetSessions");
    const data = await res.json();

    setDashData(data.clientBlobsList);
  };

  useEffect(() => {
    getDashboardData();
  }, []);

  const clientIndex = useSelector((state) => {
    return state.menu.clientIndex;
  });

  const myData = useSelector((state) => {
    return state.postData;
  });

  // console.log(myData)

  return (
    <div className="dashboard page grow">
      <Header />
      <hr />

      {dashData.map((e, i) => {
        if (clientIndex === null) {
          return <ClientData key={i} data={e} />;
        }
        return clientIndex === i && <ClientData key={i} data={e} />;
      })}
    </div>
  );
};

export default Dashboard;
