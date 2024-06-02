import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  saveClientIndex,
  saveToolsIndex,
  showDashboardData,
} from "../store/slices/MenuSlice";

import { useNavigate } from "react-router-dom";
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

  const savedClientIndex = useSelector((state) => {
    return state.menu.clientIndex;
  });

  return (
    <section className="menuBar py-4">
      <div className="logo text-center flex justify-center items-center gap-2 px-4 opacity-0  ">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          className="h-4 fill-white"
          viewBox="0 0 512 512"
        >
          <path d="M66.1 357a16 16 0 01-14.61-9.46A224 224 0 01256 32a16 16 0 0116 16v208a16 16 0 01-9.47 14.61l-189.9 84.95A15.93 15.93 0 0166.1 357z" />
          <path d="M313.59 68.18A8 8 0 00304 76v180a48.07 48.07 0 01-28.4 43.82L103.13 377a8 8 0 00-3.35 11.81 208.42 208.42 0 0048.46 50.41A206.32 206.32 0 00272 480c114.69 0 208-93.31 208-208 0-100.45-71.58-184.5-166.41-203.82z" />
        </svg>
        Logo
      </div>

      <button
        onClick={() => {
          dispatch(showDashboardData());
          navigate("/");
        }}
        className="dashBtn"
      >
        Dashboard
      </button>

      <header>Clients</header>
      <div className="clientsCover myScrollBlurY">
        <ul className="clients">
          {clientMenuData.map((e, i) => {
            return (
              <li
                key={i}
                onClick={() => {
                  dispatch(saveClientIndex(i));
                  navigate("/");
                }}
                className={i === savedClientIndex ? "active" : undefined}
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  className="ionicon"
                  viewBox="0 0 512 512"
                >
                  <path
                    d="M461.81 53.81a4.4 4.4 0 00-3.3-3.39c-54.38-13.3-180 34.09-248.13 102.17a294.9 294.9 0 00-33.09 39.08c-21-1.9-42-.3-59.88 7.5-50.49 22.2-65.18 80.18-69.28 105.07a9 9 0 009.8 10.4l81.07-8.9a180.29 180.29 0 001.1 18.3 18.15 18.15 0 005.3 11.09l31.39 31.39a18.15 18.15 0 0011.1 5.3 179.91 179.91 0 0018.19 1.1l-8.89 81a9 9 0 0010.39 9.79c24.9-4 83-18.69 105.07-69.17 7.8-17.9 9.4-38.79 7.6-59.69a293.91 293.91 0 0039.19-33.09c68.38-68 115.47-190.86 102.37-247.95zM298.66 213.67a42.7 42.7 0 1160.38 0 42.65 42.65 0 01-60.38 0z"
                    fill="none"
                    stroke="currentColor"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="32"
                  />
                  <path
                    d="M109.64 352a45.06 45.06 0 00-26.35 12.84C65.67 382.52 64 448 64 448s65.52-1.67 83.15-19.31A44.73 44.73 0 00160 402.32"
                    fill="none"
                    stroke="currentColor"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="32"
                  />
                </svg>

                <span>{e.menuItemCaption}</span>
              </li>
            );
          })}
        </ul>
      </div>

      <header>Tools</header>
      <div className="toolsCover myScrollBlurY">
        <ul className="tools">
          {toolsMenuData.map((e, i) => {
            return (
              <li key={i}>
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  className="ionicon"
                  viewBox="0 0 512 512"
                >
                  <path
                    d="M461.81 53.81a4.4 4.4 0 00-3.3-3.39c-54.38-13.3-180 34.09-248.13 102.17a294.9 294.9 0 00-33.09 39.08c-21-1.9-42-.3-59.88 7.5-50.49 22.2-65.18 80.18-69.28 105.07a9 9 0 009.8 10.4l81.07-8.9a180.29 180.29 0 001.1 18.3 18.15 18.15 0 005.3 11.09l31.39 31.39a18.15 18.15 0 0011.1 5.3 179.91 179.91 0 0018.19 1.1l-8.89 81a9 9 0 0010.39 9.79c24.9-4 83-18.69 105.07-69.17 7.8-17.9 9.4-38.79 7.6-59.69a293.91 293.91 0 0039.19-33.09c68.38-68 115.47-190.86 102.37-247.95zM298.66 213.67a42.7 42.7 0 1160.38 0 42.65 42.65 0 01-60.38 0z"
                    fill="none"
                    stroke="currentColor"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="32"
                  />
                  <path
                    d="M109.64 352a45.06 45.06 0 00-26.35 12.84C65.67 382.52 64 448 64 448s65.52-1.67 83.15-19.31A44.73 44.73 0 00160 402.32"
                    fill="none"
                    stroke="currentColor"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="32"
                  />
                </svg>

                <span>{e.menuItemCaption}</span>
              </li>
            );
          })}
        </ul>
      </div>
    </section>
  );
};

export default Menu;
