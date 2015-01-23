/** @jsx React.DOM */

var ProductsTable = React.createClass({
  render: function(){
    return (
      <div className="productsTable">
        <h1>This is my first React.js component!</h1>
      </div>
    );
  }
});

React.render(<ProductsTable/>, document.getElementById('productsTablePlaceholder'));