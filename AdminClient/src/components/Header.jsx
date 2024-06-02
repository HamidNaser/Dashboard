import React from "react";
import { useDispatch } from "react-redux";
import { toggleMenu } from "../store/slices/MenuSlice";

const Header = () => {
  const dispatch = useDispatch();

  return (
    <header className="flex items-center p-4 px-6 gap-4 sticky top-0 bg-white z-10">
      <svg
        onClick={() => dispatch(toggleMenu())}
        xmlns="http://www.w3.org/2000/svg"
        className="h-6"
        viewBox="0 0 512 512"
      >
        <rect
          x="64"
          y="64"
          width="80"
          height="80"
          rx="40"
          ry="40"
          fill="none"
          stroke="currentColor"
          strokeMiterlimit="10"
          strokeWidth="32"
        />
        <rect
          x="216"
          y="64"
          width="80"
          height="80"
          rx="40"
          ry="40"
          fill="none"
          stroke="currentColor"
          strokeMiterlimit="10"
          strokeWidth="32"
        />
        <rect
          x="368"
          y="64"
          width="80"
          height="80"
          rx="40"
          ry="40"
          fill="none"
          stroke="currentColor"
          strokeMiterlimit="10"
          strokeWidth="32"
        />
        <rect
          x="64"
          y="216"
          width="80"
          height="80"
          rx="40"
          ry="40"
          fill="none"
          stroke="currentColor"
          strokeMiterlimit="10"
          strokeWidth="32"
        />
        <rect
          x="216"
          y="216"
          width="80"
          height="80"
          rx="40"
          ry="40"
          fill="none"
          stroke="currentColor"
          strokeMiterlimit="10"
          strokeWidth="32"
        />
        <rect
          x="368"
          y="216"
          width="80"
          height="80"
          rx="40"
          ry="40"
          fill="none"
          stroke="currentColor"
          strokeMiterlimit="10"
          strokeWidth="32"
        />
        <rect
          x="64"
          y="368"
          width="80"
          height="80"
          rx="40"
          ry="40"
          fill="none"
          stroke="currentColor"
          strokeMiterlimit="10"
          strokeWidth="32"
        />
        <rect
          x="216"
          y="368"
          width="80"
          height="80"
          rx="40"
          ry="40"
          fill="none"
          stroke="currentColor"
          strokeMiterlimit="10"
          strokeWidth="32"
        />
        <rect
          x="368"
          y="368"
          width="80"
          height="80"
          rx="40"
          ry="40"
          fill="none"
          stroke="currentColor"
          strokeMiterlimit="10"
          strokeWidth="32"
        />
      </svg>

      <span className="font-bold text-xl">Dashboard</span>
    </header>
  );
};

export default Header;
