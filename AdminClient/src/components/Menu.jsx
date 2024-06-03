import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import MenuHeader from "./Menu/MenuHeader";
import ClientMenu from "./Menu/MenuClients";
import ToolsMenu from "./Menu/MenuTools";

const Menu = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const [clientMenuData, setClientMenuData] = useState([]);
  const [toolsMenuData, setToolsMenuData] = useState([]);

  const getMenuData = async () => {
    try {
      const res = await fetch("https://localhost:5001/AdminApi/GetMenu");
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
      <ToolsMenu toolsMenuData={toolsMenuData} saveToolsIndex={saveToolsIndex} />
    </section>
  );
};

export default Menu;
