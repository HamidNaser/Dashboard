import React, { useEffect, useState } from "react";
import ReactApexChart from "react-apexcharts";

const UpIcon = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" className="fill-green-500" height={15}>
      <path d="M414 321.94L274.22 158.82a24 24 0 00-36.44 0L98 321.94c-13.34 15.57-2.28 39.62 18.22 39.62h279.6c20.5 0 31.56-24.05 18.18-39.62z" />
    </svg>
  );
};

const DonutChart = ({ provider, data }) => {

  const chartData = {
    series: [500, 600],

    options: {
      chart: { type: "donut" },
      legend: { show: false },
      dataLabels: { enabled: false },
      tooltip: { enabled: false },
      fill: { colors: ["var(--pass)", "var(--fail)"] },
      states: {
        hover: { filter: { type: "lighten", value: 0.5 } },
        active: { filter: { type: "none", value: 0 } },
      },
      stroke: { width: 0 },
      plotOptions: {
        pie: {
          expandOnClick: false,
          donut: {
            size: "80%",
            labels: {
              show: false,
              name: { show: false },
            },
          },
        },
      },
    },
  };

  return (
    <div id="chart" className="flex flex-col justify-center items-center">
      <div className="relative flex justify-center items-center">
        <ReactApexChart options={chartData.options} series={chartData.series} type="donut" height={300} />
        <div className="flex justify-center flex-col items-center absolute">
          <div>
            <b className="text-2xl font-semibold">{data.total}</b> <span>{provider ? "Provider" : "Locations"}</span>
          </div>
          <div className="flex">
            <UpIcon />
            <b className="text-green-500 font-semibold">{Math.round((data.pass/data.total)*100)}%</b>
          </div>
        </div>
      </div>

      <div className="w-[350px]">
        <div className="flex items-center justify-between p-2 py-3 border-b-2 border-solid">
          <div className="flex items-center gap-4">
            <div className="bg-[var(--pass)] h-4 w-4 rounded-full"></div>
            <span>Pass</span>
          </div>
          <div>${data.pass}</div>
        </div>
        <div className="flex items-center justify-between p-2 py-3 border-b-2 border-solid">
          <div className="flex items-center gap-4">
            <div className="bg-[var(--fail)] h-4 w-4 rounded-full"></div>
            <span>Fail</span>
          </div>
          <div>${data.total - data.pass}</div>
        </div>
      </div>
      <br />
    </div>
  );
};

export default DonutChart;
