import { PriceUpdate } from "../models";
interface ComponentProps {
  price: PriceUpdate;
}

const TickerCard: React.FC<ComponentProps> = ({ price }) => {
    const getPriceClass = (price: number) => {
        return price >= 0 ? 'text-success h6' : 'text-danger h6'; // green for positive, red for negative
      };
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
        </div>
    </div>
  );
};

export default TickerCard;
