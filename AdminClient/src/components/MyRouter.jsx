import React, { useEffect } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import Dashboard from "../pages/Dashboard";
import View from "../pages/View";
import Menu from "./Menu";
import { menuOff, menuOn} from "../store/slices/MenuSlice";
import ViewInput from "../pages/ViewInput";

const MyRouter = () => {
  const isMenuRedux = useSelector((state) => state.menu.toggleMenu);
  const dispatch = useDispatch();

  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth < 980) {
        dispatch(menuOff());
      } else {
        dispatch(menuOn());
      }
    };
    window.addEventListener("resize", handleResize);
    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  return (
    <BrowserRouter>
      {isMenuRedux && <Menu />}
      <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="/view/results" element={<View />} />
        <Route path="/view/input" element={<ViewInput />} />
      </Routes>
    </BrowserRouter>
  );
};

export default MyRouter;
