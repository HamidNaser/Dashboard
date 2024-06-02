import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import "./App.css";
import MyRouter from "./components/MyRouter";
import { emptyPostState } from "./store/slices/PostSlice";

const App = () => {
  return (
    <div className="flex h-full myContainer">
      <MyRouter />
    </div>
  );
};

export default App;
