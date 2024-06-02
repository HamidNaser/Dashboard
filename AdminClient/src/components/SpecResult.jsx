import React from "react";

const SpecResult = ({ resultData, hidden }) => {
  return (
    <div hidden={hidden}>
      <section className="specResult p-4 px-6">
        <header className="text-xl font-bold">
          {resultData.providersSPECHeader}
        </header>

        <div className="tableCover">
          <table>
            <thead>
              <tr>
                {Object.keys(resultData.providersSPECTableColumnsCaptions).map(
                  (key, i) => (
                    <th key={i}>
                      {resultData.providersSPECTableColumnsCaptions[key]}
                    </th>
                  )
                )}
              </tr>
            </thead>

            <tbody>
              {resultData.providersSPEC.map((e, i) => {
                return (
                  <tr key={i}>
                    <td>{e}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
      </section>
    </div>
  );
};

export default SpecResult;
