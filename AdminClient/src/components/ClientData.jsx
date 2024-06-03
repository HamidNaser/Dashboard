import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { getResultQuery } from "../store/slices/ResultSlice";
import GraphSvg from "../assets/GraphSvg";
import DounoutChart from "./Charts/DounoutChart";
import MultiButton from "./custom/MultiButton";
import useClientData from "./useClientData";
import { setClient } from "../store/slices/dataSlice";

const TickIcon = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
      <path d="M17.4129 8.93857L10.5 16.4799L6.58712 12.2113L8.06143 10.8598L10.5 13.5201L15.9386 7.58712L17.4129 8.93857Z" fill="var(--tickGreenBg)" />
      <path fill-rule="evenodd" clip-rule="evenodd" d="M2 2H22V22H2V2ZM4 4V20H20V4H4Z" fill="var(--tickGreenBg)" />
    </svg>
  );
};

const CrossIcon = () => {
  return (
    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
      <path fill-rule="evenodd" clip-rule="evenodd" d="M2 2H22V22H2V2ZM4 4V20H20V4H4Z" fill="var(--liveDataBg)" />
      <rect fill="var(--liveDataBg)" height="2.5" rx="1.25" transform="scale(0.8) translate(15 42) rotate(-135)" width="15.435" x="11.282" y="17.75" />
      <rect fill="var(--liveDataBg)" height="2.5" rx="1.25" transform="scale(0.8) translate(-12 15) rotate(-45)" width="15.435" x="11.282" y="17.75" />
    </svg>
  );
};

const DeleteIcon = () => {
  return (
    <div className="border-[var(--liveDataBg)] border-solid border-2 p-2.5 rounded-full cursor-pointer">
      <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" viewBox="0 0 512 512">
        <path d="M112 112l20 320c.95 18.49 14.4 32 32 32h184c17.67 0 30.87-13.51 32-32l20-320" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32" />
        <path stroke="currentColor" stroke-linecap="round" stroke-miterlimit="10" stroke-width="32" d="M80 112h352" />
        <path d="M192 112V72h0a23.93 23.93 0 0124-24h80a23.93 23.93 0 0124 24h0v40M256 176v224M184 176l8 224M328 176l-8 224" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32" />
      </svg>
    </div>
  );
};

const ClientData = (props) => {
  const { data, fns } = useClientData();

  const { clientName, clientId, clientBlobsList } = props.data;

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const [selectedSession, setSelectedSession] = useState({});

  const handleResultsView = (e, path) => {
    dispatch(getResultQuery([clientId, e.blobName]));
    dispatch(setClient(clientName));
    navigate(path);
  };

  useEffect(() => {
    if (clientBlobsList?.length) {
      setSelectedSession(clientBlobsList[0]);
    }
  }, [clientBlobsList]);

  const handleSessionClick = (data) => {
    setSelectedSession(data);
  };

  return (
    <section className="clientData m-6 py-5 rounded-md grid">
      <div className="px-5 flex justify-between">
        <div>
          <div className="name font-bold text-xl">{clientName}</div>
          <div className="text-sm">
            <span className="font-semibold">Client Id :</span> {clientId}
          </div>
        </div>
      </div>

      {/* Graph  */}

      <div className="graphCover grid grid-cols-2">
        <div className="providersGraph grid gap-3 grow p-7">
          <header className="flex items-center gap-2 font-semibold">
            <GraphSvg /> PROVIDERS
          </header>
          <DounoutChart provider={true} data={{ total: selectedSession?.blobProvidersCount, pass: selectedSession?.blobProvidersCountPass }} />
        </div>

        <div className="locationsGraph grid gap-3 grow p-7">
          <header className="flex items-center gap-2 font-semibold">
            <GraphSvg /> LOCATIONS
          </header>
          <DounoutChart data={{ total: selectedSession?.blobLocationsCount, pass: selectedSession?.blobLocationsCountPass }} />
        </div>
      </div>
      <div className="flex justify-end pr-8">
        <DeleteIcon />
      </div>
      <div className="tableCover">
        <input type="range" className={"w-full opacity-0 " + (data.progress > 0 && "!opacity-100")} value={data.progress} />
        <table className="w-full">
          <thead>
            <tr>
              <th>Session Id</th>
              <th>Created Date</th>
              <th>Back Up</th>
              <th>Locations</th>
              <th>Providers</th>
              <th>Session Input</th>
              <th>Session Results</th>
              <th>Help</th>
              <th>Status</th>
              <th><input type="checkbox" className="h-4 w-4 mt-1" /></th>
            </tr>
          </thead>
          <tbody>
            {clientBlobsList.map((e, i) => {
              return (
                <tr key={i}>
                  <td onClick={() => handleSessionClick(e)} className="cursor-pointer">
                    {e.blobName}
                  </td>
                  <td>{e.blobModifiedDate}</td>
                  <td>
                    <MultiButton color={"bg-[var(--backUpBg)]"} variant={3} onDownload={() => fns.downloadBackUp(clientId, e.blobName)} onRestore={() => fns.restoreBackUp(clientId, e.blobName)}>
                      View
                    </MultiButton>
                  </td>
                  <td>{e.blobLocationsCount}</td>
                  <td>{e.blobProvidersCount}</td>
                  <td>
                    <MultiButton onView={() => handleResultsView(e, "/view/input")} color={"bg-[var(--inputBg)]"} onDownload={() => fns.downloadSessionInput(clientId, e.blobName)}>
                      View
                    </MultiButton>
                  </td>
                  <td>
                    <MultiButton onView={() => handleResultsView(e, "/view/results")} onDownload={() => fns.downloadSessionResults(clientId, e.blobName)} color={"bg-[var(--resultBg)]"}>
                      View
                    </MultiButton>
                  </td>
                  <td>
                    <button className="!bg-[var(--helpBg)]" onClick={() => fns.downloadHelp(clientId, e.blobName)}>
                      Help
                    </button>
                  </td>
                  <td>{e.status ? <TickIcon /> : <CrossIcon />}</td>
                  <td className="flex justify-center">
                    <input type="checkbox" className="h-4 w-4 mt-1" />
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
