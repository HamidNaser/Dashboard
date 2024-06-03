import React from "react";

const DownloadIcon = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32" height={20}>
      <g id="Download">
        <path className="fill-white" d="M26,21v5H6V21a1,1,0,0,1,2,0v3H24V21a1,1,0,0,1,2,0ZM10.4854,17.8574,16,21.166l5.5146-3.3086a1,1,0,0,0-1.0292-1.7148L17,18.2339V7a1,1,0,0,0-2,0V18.2339l-3.4854-2.0913a1,1,0,0,0-1.0292,1.7148Z" />
      </g>
    </svg>
  );
};

const ReloadIcon = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" height={20}>
      <path d="M400 148l-21.12-24.57A191.43 191.43 0 00240 64C134 64 48 150 48 256s86 192 192 192a192.09 192.09 0 00181.07-128" fill="none" stroke="currentColor" stroke-linecap="round" stroke-miterlimit="10" stroke-width="32" />
      <path fill="white" d="M464 97.42V208a16 16 0 01-16 16H337.42c-14.26 0-21.4-17.23-11.32-27.31L436.69 86.1C446.77 76 464 83.16 464 97.42z" />
    </svg>
  );
};

const MultiButton = ({ onView, onDownload, onRestore, color = "bg-black", variant = 1, children, className }) => {
  return (
    <div className={`flex rounded-md w-min whitespace-nowrap ${color} ${variant === 3 ? "flex-row-reverse" : ""} ` + className}>
      <button onClick={onView}>{children}</button>
      {variant == 3 && (
        <>
          <div className="border-[1px] border-solid border-white my-1"></div>
          <button onClick={onRestore}>
            <ReloadIcon />
          </button>
        </>
      )}
      <div className="border-[1px] border-solid border-white my-1"></div>
      <button onClick={onDownload}>
        <DownloadIcon />
      </button>
    </div>
  );
};

export default MultiButton;
