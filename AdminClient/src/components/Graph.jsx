import React from "react";

const Graph = ({clientBlobsList}) => {
  clientBlobsList.map((e, i) => {
    return (
      <div key={i} className="graph">
        <div className="flex items-center">
          <div>{e.blobModifiedDate}</div>
          <div className="grow grid gap-1">
            <div className="bg-gray-400">
              <div className="h-1 w-1/2 bg-blue-500"></div>
            </div>

            <div className="bg-gray-400">
              <div className="h-1 w-1/2 bg-red-500"></div>
            </div>
          </div>
        </div>
      </div>
    );
  });
};

export default Graph;
