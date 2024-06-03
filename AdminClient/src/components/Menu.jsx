import React, { useState, useEffect } from "react";
import { useSelector } from "react-redux";
import MenuHeader from "./Menu/MenuHeader";
import ClientMenu from "./Menu/MenuClients";
import ToolsMenu from "./Menu/MenuTools";

const Menu = () => {
  const [clientMenuData, setClientMenuData] = useState([]);
  const [toolsMenuData, setToolsMenuData] = useState([]);

  const getMenuData = async () => {
    try {
      const res = await fetch(`${import.meta.env.VITE_BASE_URL}/AdminApi/GetMenu`);
      const data = await res.json();

      setClientMenuData(data.menuNodes[1].menuItems);
      setToolsMenuData(data.menuNodes[2].menuItems);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    getMenuData();
  }, []);

  const savedClientIndex = useSelector((state) => state.menu.clientIndex);
  const saveToolsIndex = useSelector((state) => state.menu.toolsIndex);

  return (
    <section className="menuBar py-4">
      <MenuHeader />
      <ClientMenu clientMenuData={clientMenuData} savedClientIndex={savedClientIndex} />
    </section>
  );
};

export default Menu;
