import React from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { getResultQuery } from "../store/slices/ResultSlice";
import GraphSvg from "../assets/GraphSvg";

const ClientData = (props) => {
  const { clientName, clientId, clientBlobsList } = props.data;

  const dispatch = useDispatch();
  const navigate = useNavigate();

  return (
    <section className="clientData m-6 py-5 rounded-md grid gap-6">
      <div className="px-5">
        <div className="name font-bold text-xl">{clientName}</div>
        <div className="text-sm">
          <span className="font-semibold">Client Id :</span> {clientId}
        </div>
      </div>

      {/* Graph  */}

      <div className="graphCover px-5 flex gap-8 ">
        <div className="providersGraph grid gap-3 grow">
          <header className="flex items-center gap-2 font-semibold">
            <GraphSvg /> PROVIDERS
          </header>
          {clientBlobsList.map((e, i) => {
            const providers =
              (e.blobProvidersCount / e.blobProvidersCount) * 100;

            const providerPass =
              (e.blobProvidersCountPass / e.blobProvidersCount) * 100;

            return (
              <div key={i} className="graph flex items-center gap-2">
                <div>{e.blobModifiedDate}</div>
                <div className="grow grid gap-1">
                  <div className="bg-gray-400">
                    <div
                      className="h-1 bg-blue-500"
                      style={{ width: `${providerPass}%` }}
                    ></div>
                  </div>

                  <div className="bg-gray-400">
                    <div
                      className="h-1 bg-red-500"
                      style={{ width: `${providers}%` }}
                    ></div>
                  </div>
                </div>
              </div>
            );
          })}
        </div>

        <div className="locationsGraph grid gap-3 grow">
          <header className="flex items-center gap-2 font-semibold">
            <GraphSvg /> LOCATIONS
          </header>
          {clientBlobsList.map((e, i) => {
            const locations =
              (e.blobLocationsCount / e.blobLocationsCount) * 100;

            const locationsPass =
              (e.blobLocationsCountPass / e.blobLocationsCount) * 100;

            return (
              <div key={i} className="graph flex items-center gap-2">
                <div>{e.blobModifiedDate}</div>
                <div className="grow grid gap-1">
                  <div className="bg-gray-400">
                    <div
                      className="h-1 bg-blue-500"
                      style={{ width: `${locationsPass}%` }}
                    ></div>
                  </div>

                  <div className="bg-gray-400">
                    <div
                      className="h-1 bg-red-500"
                      style={{ width: `${locations}%` }}
                    ></div>
                  </div>
                </div>
              </div>
            );
          })}
        </div>
      </div>

      <div className="tableCover">
        <table className="w-full">
          <thead>
            <tr>
              <th>SESSION ID</th>
              <th>DATE</th>
              <th>PROVIDERS</th>
              <th>LOCATIONS</th>
              <th>INPUT</th>
              <th>RESULT</th>
            </tr>
          </thead>
          <tbody>
            {clientBlobsList.map((e, i) => {
              return (
                <tr key={i}>
                  <td>{e.blobName}</td>
                  <td>{e.blobModifiedDate}</td>
                  <td>{e.blobProvidersCount}</td>
                  <td>{e.blobLocationsCount}</td>
                  <td>
                    <button>Input</button>
                  </td>
                  <td>
                    <button
                      onClick={() => {
                        dispatch(getResultQuery([clientId, e.blobName]));
                        navigate("/result");
                      }}
                    >
                      Result
                    </button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </section>
  );
};

export default ClientData;
