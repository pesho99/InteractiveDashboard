import { PriceUpdate } from "../models";
interface ComponentProps {
  price: PriceUpdate;
}

const TickerCard: React.FC<ComponentProps> = ({ price }) => {
    const getPriceClass = (price: number) => {
        return price >= 0 ? 'text-success h6' : 'text-danger h6'; // green for positive, red for negative
      };

      const getDate = (date: Date | undefined) =>
      {
        if (date == null)
        {
          return ''
        }
        const currentDate = new Date();
        const year = currentDate.getFullYear();
        const month = String(currentDate.getMonth() + 1).padStart(2, "0"); // Months are zero-indexed
        const day = String(currentDate.getDate()).padStart(2, "0");
        const hours = String(currentDate.getHours()).padStart(2, "0");
        const minutes = String(currentDate.getMinutes()).padStart(2, "0");
        const seconds = String(currentDate.getSeconds()).padStart(2, "0");
        
        const formattedDateTime = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
        return formattedDateTime
      }
  return (
    <div className="col-md-3">
      <div className="card bg-dark text-white mb-3">
        <h5 className="card-header h2">{price.symbolName}</h5>
        <div className="card-body row">
            <div className="col-md-6">
            <p className="h4"><strong>Bid Price:</strong> ${price.bid.toFixed(2)}</p>
            <p className={getPriceClass(price.bidchange)}><strong>Change:</strong> ${price.bidchange?.toFixed(2)}</p>
            </div>
            <div className="col-md-6">
              <p className="h4"><strong>Ask Price:</strong> ${price.ask.toFixed(2)}</p>
              <p className={getPriceClass(price.askchange)} ><strong>Change:</strong> ${price.askchange?.toFixed(2)}</p>
            </div>
          </div>
          <p className="h4"><strong>Last Change:</strong> {getDate(price.priceDate)}</p>
        </div>
    </div>
  );
};

export default TickerCard;
